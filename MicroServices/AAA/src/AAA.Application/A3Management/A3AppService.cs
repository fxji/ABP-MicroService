using AAA.A3Management.Dto;
using AAA.Models;
using AAA.Permissions;
using Email.SendEmail;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;


namespace AAA.A3Management
{
    [Authorize(AAAPermissions.A3.Default)]
    public class A3AppService : ApplicationService, IA3AppService
    {
        private const string FormName = "A3";
        //private IRepository<A3, Guid> _repository;
        private readonly ISendEmailAppService _sendEmailAppService;
        private readonly IDistributedEventBus _distributedEventBus;
        private readonly A3Manager _a3Manager;

        public A3AppService(
            IRepository<A3, Guid> repository, ISendEmailAppService sendEmailAppService, IDistributedEventBus distributedEventBus, A3Manager a3Manager
            )
        {
            //_repository = repository; 
            _sendEmailAppService = sendEmailAppService; _distributedEventBus = distributedEventBus; _a3Manager = a3Manager;
        }
        #region 增删改查基础方法

        public async Task<A3Dto> Get(Guid id)
        {

            var data = await _a3Manager.GetAsync(id);
            var dto = ObjectMapper.Map<A3, A3Dto>(data);
            return dto;

        }


        /// <summary>
        /// 新的方法
        /// https://www.jianshu.com/p/891903cd764e
        /// 直接生成FileStreamResult
        /// 避免在服务端生成文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public async Task<ExportDto> Export(Guid id)
        //{
        //    var path = await _a3Manager.Export(id);
        //    return new ExportDto()
        //    {
        //        Path ="http://localhost:44308"+ path
        //    };
        //}

        public async Task<string> Export(Guid id)
        {
            return await _a3Manager.Export(id);
        }

        public async Task<PagedResultDto<A3Dto>> GetAll(GetA3InputDto input)
        {
            var query = (await _a3Manager.GetAll())

                .WhereIf(!(input.A3Id == Guid.Empty || input.A3Id is null), a => a.Id.Equals(input.A3Id))
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), a => a.Name.Contains(input.Filter))
                .WhereIf(!string.IsNullOrWhiteSpace(input.Source), a => a.Source.Equals(input.Source))
                .WhereIf(!string.IsNullOrWhiteSpace(input.Process), a => a.Process.Equals(input.Process))
                .WhereIf(!(input.OrganizationId == Guid.Empty || input.OrganizationId is null), a => a.OrganizationId.Equals(input.OrganizationId))
                ;

            var totalCount = query.Count();
            var items =  query.OrderBy(input.Sorting ?? "Id")
                       .Skip(input.SkipCount)
                       .Take(input.MaxResultCount)
                       .ToList();

            var dto = ObjectMapper.Map<List<A3>, List<A3Dto>>(items);
            return new PagedResultDto<A3Dto>(totalCount, dto);
        }

        public async Task<A3Dto> DataPost(CreateOrUpdateA3Dto input)
        {
            A3 result = null;
            if (!input.Id.HasValue)
            {
                input.Id = GuidGenerator.Create();
                result = await _a3Manager.Insert(ObjectMapper.Map<CreateOrUpdateA3Dto, A3>(input));
            }
            else
            {
                var data = await _a3Manager.GetAsync(input.Id.Value);
                result = await _a3Manager.Update(ObjectMapper.Map(input, data));
            }



            return ObjectMapper.Map<A3, A3Dto>(result);
        }

        public async Task Share(ShareDto input)
        {

            await _sendEmailAppService.Send(
                 new EmailSendingArgs
                 {
                     EmailAddress = input.EmailAddress,
                     Subject = "Message u need to known",
                     Body = $"A3Report {input.A3.Name} was created, pls check {_a3Manager.FrontEndUrl}/#/A3?a3id={input.A3.Id} and confirm"
                 }
            );

            //await _distributedEventBus.PublishAsync(
            //    new StockCountChangedEto
            //    {
            //        ProductId = productId,
            //        NewCount = newCount
            //    }
            //);
        }

        public async Task Delete(List<Guid> ids)
        {
            await _a3Manager.Delete(ids);

        }


        #endregion

    }
}