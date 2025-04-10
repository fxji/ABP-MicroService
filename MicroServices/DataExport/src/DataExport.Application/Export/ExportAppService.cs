using DataExport.ExportManagement;
using DataExport.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.Util;
using NPOI.XSSF.UserModel;
using SixLabors.ImageSharp;
using System;
//using System.Drawing;
using System.IO;

namespace DataExport.Export
{
    public class ExportAppService : DataExportAppService, IExportAppService
    {
        private const int HEADERROWINDEX = 1;
        private const int FOOTERROWINDEX = 16;
        private const string SHEET_SUMMARY = "Summary";

        private int _issueIndex = 4;
        private int _issueSymptomDescriptionIndex = 12;
        private int _issueProblemDescriptionIndex = 16;
        private int _issueGoalStatementIndex = 35;

        private int _containmentIndex = 24;
        private int _riskIndex = 31;
        private int _causeIndex = 55;
        private int _correctActionIndex = 6;

        private int _readAcrossIndex = 55;
        private int _footerIndex = 72;

        private int _imageRowIndex = 7;
        private int _imageColmnIndex = CellReference.ConvertColStringToIndex("I");


        private static IHostEnvironment _environment;

        private readonly DataExportOptions _exportOptions;

        private XSSFWorkbook workbook;

        private ICellStyle DateStyle
        {
            //must after OpenExcel
            get
            {
                var cellStyle = this.workbook.CreateCellStyle();
                IDataFormat format = this.workbook.CreateDataFormat();
                cellStyle.DataFormat = format.GetFormat("yyyy-m-d");
                return cellStyle;
            }
        }

        public ExportAppService(IOptions<DataExportOptions> options, IHostEnvironment hostEnvironment)
        {

            //_shoSchoolUserInfoContext = schoolUserInfoContext;
            _environment = hostEnvironment;
            _exportOptions = options.Value;

        }

        //TODO:make it as async
        public byte[] Export(InputExDto input)
        {
            OpenExcel();

            WriteHeader(input.A3.Id.ToString(), input.A3.Name);

            input.Issues.ForEach(item => WriteIssue(item.Name, item.CustomerGroup, item.Description, item.OccurrenceDate, item.Project, item.Type, item.Rate, item.SymptomDescription, item.GoalStatement));
            input.ContainmentActions.ForEach(item => WriteContainmentAction(item.Type, item.Name, item.Responsibility, item.Status));
            input.RiskAssessments.ForEach(item => WriteRisk(item.Name, item.Description, item.Probability, item.Functionally, item.SafetyRelevant));
            input.Causes.ForEach(item => WriteCause(item.Type, item.Name, item.Status));
            input.CorrectiveActions.ForEach(item => WriteCorrectiveActions(item.Name, item.Responsibility, item.DueDate, item.Status));
            input.ConfirmInfos.ForEach(item => WriteReadAcross(item.Comments, item.CreatorId.Value.ToString(), item.CreationTime));

            input.A3Attachments.ForEach(item => WriteImage(item.Content));

            WriteFooter(input.A3.OrganizationId.ToString(), input.A3.UserEmail, input.A3.StartDate);

            return ConvertToBytes();
        }

        private byte[] ConvertToBytes()
        {
            ByteArrayOutputStream bos = new ByteArrayOutputStream();
            workbook.Write(bos);
            return bos.ToByteArray();
            //throw new NotImplementedException();
        }



        private void OpenExcel()
        {
            //"D:\\Projects\\ABP-MicroService\\MicroServices\\AAA\\host\\AAA.HttpApi.Host/"
            var loadPath = _environment.ContentRootPath + "\\" + _exportOptions.TemplatePath + "\\";//>>>相当于HttpContext.Current.Server.MapPath("") 
            //首先读取Excel文件
            var excelStream = File.Open(@loadPath + "template.xlsx", FileMode.Open, FileAccess.Read);
            this.workbook = new XSSFWorkbook(excelStream);
        }

        private void WriteToSheet(string name)
        {

        }

        private void WriteHeader(string id, string title)
        {
            var sheet = this.workbook.GetSheet(SHEET_SUMMARY);
            //id
            var d = CellReference.ConvertColStringToIndex("A");
            var y = CellReference.ConvertColStringToIndex("C");
            sheet.GetRow(HEADERROWINDEX).GetCell(d).SetCellValue("ID:" + id);
            sheet.GetRow(HEADERROWINDEX).GetCell(y).SetCellValue(title);
        }

        private void WriteIssue(string name,
            string customerGroup,
            string description,
            DateTime occurrenceDate,
            string project,
            string type,
            float rate,
            string symptomDescription,
            string goalStatement
            )
        {
            var sheet = this.workbook.GetSheet(SHEET_SUMMARY);
            var b = CellReference.ConvertColStringToIndex("B");

            sheet.GetRow(this._issueIndex++).GetCell(b).SetCellValue(customerGroup);
            sheet.GetRow(this._issueIndex++).GetCell(b).SetCellValue(project);
            sheet.GetRow(this._issueIndex++).GetCell(b).SetCellValue(customerGroup);
            sheet.GetRow(this._issueIndex++).GetCell(b).SetCellValue(project);
            sheet.GetRow(this._issueIndex++).GetCell(b).SetCellValue(type);
            sheet.GetRow(this._issueIndex++).GetCell(b).SetCellValue(rate);
            sheet.GetRow(this._issueIndex).GetCell(b).CellStyle = this.DateStyle;
            sheet.GetRow(this._issueIndex++).GetCell(b).SetCellValue(occurrenceDate);
            sheet.GetRow(this._issueSymptomDescriptionIndex).GetCell(0).SetCellValue(symptomDescription);
            sheet.GetRow(this._issueProblemDescriptionIndex).GetCell(0).SetCellValue(description);
            sheet.GetRow(this._issueGoalStatementIndex).GetCell(0).SetCellValue(goalStatement);
            //throw new NotImplementedException();
        }

        //[Obsolete("use Export(InputExDto input) instead")]
        //public string Export(string excelName)
        //{
        //    return WriteToFile(excelName, this.workbook);
        //}


        //[Obsolete("use Export(InputExDto input) instead")]
        //private bool Export(out string resultMsg, out string excelFilePath)
        //{
        //    var result = true;
        //    excelFilePath = "";
        //    resultMsg = "successfully";
        //    //Excel导出名称
        //    string excelName = "A3-Report";
        //    try
        //    {
        //        //excelFilePath = CreateNew(excelName);

        //        excelFilePath = CreateFromTemplate(excelName);
        //    }
        //    catch (Exception e)
        //    {
        //        result = false;
        //        resultMsg = e.Message;
        //    }
        //    return result;
        //}

        //private string CreateFromTemplate(string excelName)
        //{

        //    string excelFilePath;
        //    var loadPath = _environment.ContentRootPath + "/";//>>>相当于HttpContext.Current.Server.MapPath("") 
        //                                                      ////首先读取Excel文件
        //                                                      //var excelStream = File.Open(@loadPath + "template.xlsx", FileMode.Open, FileAccess.Read);
        //                                                      //var workbook = new XSSFWorkbook(excelStream);




        //    excelFilePath = WriteToFile(excelName, workbook);

        //    return excelFilePath;
        //}

        //private int LoadImage(Stream file, XSSFWorkbook workbook)
        //{
        //    //FileStream fileStream = new FileStream("path", FileMode.Create, FileAccess.Write);
        //    //file.CopyTo(fileStream);
        //    byte[] buffer = new byte[file.Length];
        //    file.Read(buffer, 0, (int)file.Length);
        //    return workbook.AddPicture(buffer, PictureType.PNG);
        //}

        //private string WriteToFile(string excelName, XSSFWorkbook workbook)
        //{
        //    string excelFilePath;
        //    string folder = DateTime.Now.ToString("yyyyMMdd");


        //    //保存文件到静态资源文件夹中（wwwroot）,使用绝对路径
        //    var uploadPath = _environment.ContentRootPath + "/UploadFile/" + folder + "/";

        //    //excel保存文件名
        //    string excelFileName = excelName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

        //    //创建目录文件夹
        //    if (!Directory.Exists(uploadPath))
        //    {
        //        Directory.CreateDirectory(uploadPath);
        //    }

        //    //Excel的路径及名称
        //    string excelPath = uploadPath + excelFileName;

        //    //使用FileStream文件流来写入数据（传入参数为：文件所在路径，对文件的操作方式，对文件内数据的操作）
        //    var fileStream = new FileStream(excelPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

        //    //向Excel文件对象写入文件流，生成Excel文件
        //    workbook.Write(fileStream);


        //    //关闭文件流
        //    fileStream.Close();

        //    //释放流所占用的资源
        //    fileStream.Dispose();

        //    //excel文件保存的相对路径，提供前端下载
        //    var relativePositioning = "/UploadFile/" + folder + "/" + excelFileName;

        //    excelFilePath = relativePositioning;
        //    return excelPath;
        //}

        private void WriteRisk(string name, string description, string probability, string functionally, bool safetyRelevant)
        {
            var sheet = this.workbook.GetSheet(SHEET_SUMMARY);
            var factorIndex = CellReference.ConvertColStringToIndex("A");
            var descriptionIndex = CellReference.ConvertColStringToIndex("D");
            var probabilityIndex = CellReference.ConvertColStringToIndex("I");
            var functionallyIndex = CellReference.ConvertColStringToIndex("J");
            var safetyRelevantIndex = CellReference.ConvertColStringToIndex("K");

            sheet.GetRow(this._riskIndex).GetCell(factorIndex).SetCellValue(name);
            sheet.GetRow(this._riskIndex).GetCell(descriptionIndex).SetCellValue(description);
            sheet.GetRow(this._riskIndex).GetCell(probabilityIndex).SetCellValue(probability);
            sheet.GetRow(this._riskIndex).GetCell(functionallyIndex).SetCellValue(functionally);
            sheet.GetRow(this._riskIndex).GetCell(safetyRelevantIndex).SetCellValue(safetyRelevant);

            this._riskIndex++;
        }

        private void WriteCause(string type, string name, string status)
        {
            var sheet = this.workbook.GetSheet(SHEET_SUMMARY);
            var typeIndex = CellReference.ConvertColStringToIndex("B");
            var nameIndex = CellReference.ConvertColStringToIndex("C");
            var verifyIndex = CellReference.ConvertColStringToIndex("F");
            var statusIndex = CellReference.ConvertColStringToIndex("J");

            sheet.GetRow(this._causeIndex).GetCell(typeIndex).SetCellValue(type);
            sheet.GetRow(this._causeIndex).GetCell(nameIndex).SetCellValue(name);
            sheet.GetRow(this._causeIndex).GetCell(statusIndex).SetCellValue(status);

            this._causeIndex++;
        }

        private void WriteContainmentAction(string type, string name, string responsibility, string status)
        {
            var columnIndex = -1;
            var rowIndex = 24;
            switch (type)
            {
                case "Raw Material":
                    //case "1":
                    columnIndex = CellReference.ConvertColStringToIndex("B");
                    break;
                case "Production Line":
                    //case "2":
                    columnIndex = CellReference.ConvertColStringToIndex("D");
                    break;

                case "Warehouse":
                    //case "3":
                    columnIndex = CellReference.ConvertColStringToIndex("F");
                    break;

                case "3rd Party Warehouse":
                    //case "5":
                    columnIndex = CellReference.ConvertColStringToIndex("H");
                    break;
                case "Customer Stock":
                    //case "6":
                    columnIndex = CellReference.ConvertColStringToIndex("J");
                    break;

                default:
                    //return;
                    throw new NotImplementedException();



            }


            var sheet = this.workbook.GetSheet(SHEET_SUMMARY);

            sheet.GetRow(rowIndex++).GetCell(columnIndex).SetCellValue(name);
            sheet.GetRow(rowIndex++).GetCell(columnIndex).SetCellValue(responsibility);
            sheet.GetRow(rowIndex++).GetCell(columnIndex).SetCellValue(status);

        }

        private void WriteCorrectiveActions(string name, string responsibility, DateTime dueDate, string status)
        {
            var sheet = this.workbook.GetSheet(SHEET_SUMMARY);

            var nameIndex = CellReference.ConvertColStringToIndex("M");
            var activitiesIndex = CellReference.ConvertColStringToIndex("O");
            var responsibilityIndex = CellReference.ConvertColStringToIndex("T");
            var dueDateIndex = CellReference.ConvertColStringToIndex("U");
            var statusIndex = CellReference.ConvertColStringToIndex("V");

            sheet.GetRow(_correctActionIndex).GetCell(nameIndex).SetCellValue(name);
            sheet.GetRow(_correctActionIndex).GetCell(responsibilityIndex).SetCellValue(responsibility);
            sheet.GetRow(_correctActionIndex).GetCell(dueDateIndex).SetCellValue(dueDate);
            sheet.GetRow(_correctActionIndex).GetCell(dueDateIndex).CellStyle = this.DateStyle;// (dueDate);
            sheet.GetRow(_correctActionIndex).GetCell(statusIndex).SetCellValue(status);

            this._correctActionIndex++;
        }

        private void WriteReadAcross(string comments, string creatorId, DateTime creationTime)
        {
            var sheet = this.workbook.GetSheet(SHEET_SUMMARY);
            var activitiesIndex = CellReference.ConvertColStringToIndex("O");
            var creatorIndex = CellReference.ConvertColStringToIndex("T");
            var timeIndex = CellReference.ConvertColStringToIndex("U");

            sheet.GetRow(_readAcrossIndex).GetCell(activitiesIndex).SetCellValue(comments);
            sheet.GetRow(_readAcrossIndex).GetCell(creatorIndex).SetCellValue(creatorId);
            sheet.GetRow(_readAcrossIndex).GetCell(timeIndex).SetCellValue(creationTime);
            sheet.GetRow(_readAcrossIndex).GetCell(timeIndex).CellStyle = this.DateStyle;// (creationTime);


            this._readAcrossIndex += 4;
        }

        private void WriteFooter(string v, string userEmail, DateTime? startDate)
        {
            var sheet = this.workbook.GetSheet(SHEET_SUMMARY);
            var locationIndex = CellReference.ConvertColStringToIndex("C");
            var sponsorIndex = CellReference.ConvertColStringToIndex("H");
            var dateIndex = CellReference.ConvertColStringToIndex("R");

            sheet.GetRow(_footerIndex).GetCell(locationIndex).SetCellValue(v);
            sheet.GetRow(_footerIndex + 1).GetCell(locationIndex).SetCellValue(v);

            sheet.GetRow(_footerIndex).GetCell(sponsorIndex).SetCellValue(userEmail);
            sheet.GetRow(_footerIndex + 1).GetCell(sponsorIndex).SetCellValue(userEmail);

            sheet.GetRow(_footerIndex).GetCell(dateIndex).SetCellValue(startDate.Value);
            sheet.GetRow(_footerIndex).GetCell(dateIndex).CellStyle = DateStyle;// (startDate.Value);
            sheet.GetRow(_footerIndex + 1).GetCell(dateIndex).SetCellValue(startDate.Value);
            sheet.GetRow(_footerIndex + 1).GetCell(dateIndex).CellStyle = DateStyle;// (startDate.Value);
        }

        private void WriteImage(byte[] imageBytes)
        {
            //读取工作表，也就是Excel中的sheet，给工作表赋一个名称(Excel底部名称)
            var sheet = this.workbook.GetSheet(SHEET_SUMMARY);
            //id

            // 添加图片到Excel
            //IDrawing drawing = sheet.CreateDrawingPatriarch();
            //IClientAnchor anchor = drawing.CreateAnchor(500, 200, 500, 200, CellReference.ConvertColStringToIndex("I"), this._imageStartIndex, CellReference.ConvertColStringToIndex("J"), this._imageEndIndex);
            //int pictureIdx = workbook.AddPicture(imageBytes, PictureType.JPEG);
            //XSSFPicture pictrue = (XSSFPicture)drawing.CreatePicture(anchor, pictureIdx);

            //pictrue.LineStyle = LineStyle.DashDotGel;
            //pictrue.Resize();

            ICell target = sheet.GetRow(_imageRowIndex).GetCell(_imageColmnIndex);
            InsertCellImage(target, imageBytes);
            this._imageRowIndex += 21;


            //throw new NotImplementedException();
        }


        /// <summary>
        /// 图片在单元格等比缩放居中显示
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="value">图片二进制流</param>
        private void InsertCellImage(ICell cell, byte[] value)
        {
            if (value.Length == 0) return;//空图片处理
            double scalx = 0;//x轴缩放比例
            double scaly = 0;//y轴缩放比例
            int Dx1 = 0;//图片左边相对excel格的位置(x偏移) 范围值为:0~1023,超过1023就到右侧相邻的单元格里了
            int Dy1 = 0;//图片上方相对excel格的位置(y偏移) 范围值为:0~256,超过256就到下方的单元格里了
            bool bOriginalSize = false;//是否显示图片原始大小 true表示图片显示原始大小  false表示显示图片缩放后的大小
            ///计算单元格的长度和宽度
            double CellWidth = 0;
            double CellHeight = 0;
            int RowSpanCount;// =  cell.GetSpan().RowSpan;//合并的单元格行数
            int ColSpanCount;//=  cell.GetSpan().ColSpan;//合并的单元格列数 
            cell.IsMergeCell(out RowSpanCount, out ColSpanCount);
            int j = 0;
            for (j = 0; j < RowSpanCount; j++)//根据合并的行数计算出高度
            {
                CellHeight += cell.Sheet.GetRow(cell.RowIndex + j).Height;
            }
            for (j = 0; j < ColSpanCount; j++)
            {
                CellWidth += cell.Row.Sheet.GetColumnWidth(cell.ColumnIndex + j);
            }
            //单元格长度和宽度与图片的长宽单位互换是根据实例得出
            CellWidth = CellWidth / 35;
            CellHeight = CellHeight / 15;
            ///计算图片的长度和宽度
            MemoryStream ms = new MemoryStream(value);
            Image Img = Image.Load(ms);// Bitmap.FromStream(ms, true);
            double ImageOriginalWidth = Img.Width;//原始图片的长度
            double ImageOriginalHeight = Img.Height;//原始图片的宽度
            double ImageScalWidth = 0;//缩放后显示在单元格上的图片长度
            double ImageScalHeight = 0;//缩放后显示在单元格上的图片宽度
            if (CellWidth > ImageOriginalWidth && CellHeight > ImageOriginalHeight)//单元格的长度和宽度比图片的大，说明单元格能放下整张图片，不缩放
            {
                ImageScalWidth = ImageOriginalWidth;
                ImageScalHeight = ImageOriginalHeight;
                bOriginalSize = true;
            }
            else//需要缩放，根据单元格和图片的长宽计算缩放比例
            {
                bOriginalSize = false;
                if (ImageOriginalWidth > CellWidth && ImageOriginalHeight > CellHeight)//图片的长和宽都比单元格的大的情况
                {
                    double WidthSub = ImageOriginalWidth - CellWidth;//图片长与单元格长的差距
                    double HeightSub = ImageOriginalHeight - CellHeight;//图片宽与单元格宽的差距
                    if (WidthSub > HeightSub)//长的差距比宽的差距大时,长度x轴的缩放比为1，表示长度就用单元格的长度大小，宽度y轴的缩放比例需要根据x轴的比例来计算
                    {
                        scalx = 1;
                        scaly = (CellWidth / ImageOriginalWidth) * ImageOriginalHeight / CellHeight;//计算y轴的缩放比例,CellWidth / ImageWidth计算出图片整体的缩放比例,然后 * ImageHeight计算出单元格应该显示的图片高度,然后/ CellHeight就是高度的缩放比例
                    }
                    else
                    {
                        scaly = 1;
                        scalx = (CellHeight / ImageOriginalHeight) * ImageOriginalWidth / CellWidth;
                    }
                }
                else if (ImageOriginalWidth > CellWidth && ImageOriginalHeight < CellHeight)//图片长度大于单元格长度但图片高度小于单元格高度，此时长度不需要缩放，直接取单元格的，因此scalx=1，但图片高度需要等比缩放
                {
                    scalx = 1;
                    scaly = (CellWidth / ImageOriginalWidth) * ImageOriginalHeight / CellHeight;
                }
                else if (ImageOriginalWidth < CellWidth && ImageOriginalHeight > CellHeight)//图片长度小于单元格长度但图片高度大于单元格高度，此时单元格高度直接取单元格的，scaly = 1,长度需要等比缩放
                {
                    scaly = 1;
                    scalx = (CellHeight / ImageOriginalHeight) * ImageOriginalWidth / CellWidth;
                }
                ImageScalWidth = scalx * CellWidth;
                ImageScalHeight = scaly * CellHeight;
            }
            Dx1 = Convert.ToInt32((CellWidth - ImageScalWidth) / CellWidth * 1023 / 2);
            Dy1 = Convert.ToInt32((CellHeight - ImageScalHeight) / CellHeight * 256 / 2);
            int pictureIdx = cell.Sheet.Workbook.AddPicture((Byte[])value, PictureType.PNG);
            IClientAnchor anchor = cell.Sheet.Workbook.GetCreationHelper().CreateClientAnchor();
            anchor.AnchorType = AnchorType.MoveDontResize;
            anchor.Col1 = cell.ColumnIndex;
            anchor.Col2 = cell.ColumnIndex + ColSpanCount;// cell.GetSpan().ColSpan;
            anchor.Row1 = cell.RowIndex;
            anchor.Row2 = cell.RowIndex + RowSpanCount;// cell.GetSpan().RowSpan;
            anchor.Dy1 = Dy1;//图片下移量
            anchor.Dx1 = Dx1;//图片右移量，通过图片下移和右移，使得图片能居中显示，因为图片不同文字，图片是浮在单元格上的，文字是钳在单元格里的
            IDrawing patriarch = cell.Sheet.CreateDrawingPatriarch();
            IPicture pic = patriarch.CreatePicture(anchor, pictureIdx);
            if (bOriginalSize)
            {
                pic.Resize();//显示图片原始大小 
            }
            else
            {
                pic.Resize(scalx, scaly);//等比缩放   
            }
        }
    }
}
