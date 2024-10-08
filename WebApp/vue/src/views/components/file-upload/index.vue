<template>
    <div class="app-container">
        <div class="head-container">
            <el-button class="filter-item" size="mini" type="primary" icon="el-icon-plus" @click="handleUpload">上传文件
            </el-button>
        </div>
        <el-dialog :visible.sync="dialogFormVisible" :close-on-click-modal="false" title="上传文件" @close="cancel()"
            width="500px">
            <el-form ref="form" :inline="true" :model="form" :rules="rules" size="small" label-width="66px">
                <el-form-item label="文件名" prop="name">
                    <el-input v-model="form.name" style="width: 370px;" />
                </el-form-item>
                <el-form-item label="上传">
                    <el-upload ref="upload" :limit="1" :before-upload="beforeUpload" :auto-upload="false"
                        :on-success="handleSuccess" :on-error="handleError"
                        :action="storageApi + '/api/storage/file/upload?name=' + form.name">
                        <div class="upload">
                            <i class="el-icon-upload" /> 添加文件
                        </div>
                        <div slot="tip" class="el-upload__tip">上传文件不超过100M</div>
                    </el-upload>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button size="small" type="text" @click="cancel">取消</el-button>
                <el-button size="small" v-loading="formLoading" type="primary" @click="upload">确认</el-button>
            </div>
        </el-dialog>
        <div style="margin-top: 6px;">
            <attachment ref="attachment" :Id="Id" :category="category"></attachment>
        </div>
    </div>
</template>

<script>
import Attachment from './../attachment/index.vue'
import config from "../../../../static/config";


const defaultForm = {
    id: null,
    name: null,
};

export default {
    name: 'FileUpload',
    props: {
        category: {
            type: String,
            required: true
        },
        Id: {
            type: String,
            required: true
        },
    },
    // watch: {
    //     a3Id: {
    //         handler: function (newVal, oldVal) {
    //             this.getAttachment();
    //         },
    //         immediate: true
    //     }
    // },
    components: { Attachment },
    data() {
        return {
            rules: {
                name: [{ required: true, message: "请输入文件名", trigger: "blur" }],
            },
            form: Object.assign({}, defaultForm),
            storageApi: config.storage.ip,
            docs: [],
            dialogFormVisible: false,
            formLoading: false,
        }
    },

    created() {
    },
    methods: {
        // 上传文件
        upload() {
            this.$refs.form.validate(valid => {
                if (valid) {
                    this.formLoading = true;
                    this.$refs.upload.submit();


                }
            })

        },
        beforeUpload(file) {
            let isLt2M = true;
            isLt2M = file.size / 1024 / 1024 < 100;
            if (!isLt2M) {
                this.loading = false;
                this.$message.error("上传文件大小不能超过 100MB!");
            }
            this.form.name = file.name;
            return isLt2M;
        },
        handleSuccess(response, file, fileList) {


            let item = {
                A3Id: this.Id,
                Type: response.url.includes('/Image/') ? 'Image' : 'File',
                category: this.category,
                Name: response.name,
                Url: response.url
            }
            this.$axios.posts('api/AAA/A3Attachment/data-post', item).then(response => {
                this.$notify({
                    title: '成功',
                    message: '新增成功',
                    type: 'success',
                    duration: 2000
                });
                this.$refs.attachment.getAttachment();
            }).catch(() => {
            });

            this.form = Object.assign({}, defaultForm);
            this.$refs.form.clearValidate();
            this.$refs.upload.clearFiles();
            this.formLoading = false;
            this.dialogFormVisible = false;

            //   this.getList();
        },
        // 监听上传失败
        handleError(e, file, fileList) {
            this.$notify({
                title: e,
                type: "error",
                duration: 4000,
            });
            this.loading = false;
        },

        handleUpload() {
            this.dialogFormVisible = true;
        },


        cancel() {
            this.form = Object.assign({}, defaultForm);
            this.dialogFormVisible = false;
            this.$refs.form.clearValidate();
        },

        // getAttachment() {
        //     let query = {
        //         a3Id: this.a3Id,
        //         category: this.category,
        //         type: 'Doc'

        //     }
        //     this.$axios.gets('api/AAA/A3Attachment', query).then(response => {
        //         this.docs = response.items;
        //     });
        // },
    }
}
</script>
