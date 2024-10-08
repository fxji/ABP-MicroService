<template>
    <div>

        <!-- :action="storageApi + '/api/storage/image/upload-img?name=' + Date.now()" :on-success="handleSuccess" -->
        <el-upload ref="IssueUpload" :file-list="images"
            :action="storageApi + '/api/storage/image/upload-img?name=' + fileName" :before-upload="getFileName"
            :auto-upload="true" :on-success="handleSuccess" :on-preview="handlePreview" list-type="picture-card"
            :disabled="false">
            <i class="el-icon-plus"></i>
        </el-upload>
        <el-dialog :visible.sync="dialogVisible">
            <img width="100%" :src="dialogImageUrl" alt="">
        </el-dialog>
    </div>
</template>

<script>
import config from "../../../../static/config";
export default {
    name: 'ImageUpload',
    props: {
        category: {
            type: String,
            default: 'u did not input type',
            required: true
        },
        a3Id: {
            type: String,
            default: 'u did not input id',
            required: true
        },
    },
    watch: {
        a3Id: {
            handler: function (newVal, oldVal) {
                this.getAttachment();
            },
            immediate: true
        }
    },
    data() {
        return {
            storageApi: config.storage.ip,
            images: [],
            dialogImageUrl: '',
            dialogVisible: false,
            fileName: '',
        }
    },
    created() {
    },
    methods: {
        getFileName(file) {
            this.filename = file.name;
            this.$confirm("文件名字" + this.filename + "?", "提示", {
                confirmButtonText: "确定",
                cancelButtonText: "取消",
                type: "warning"
            }).then(
                this.$axios.posts(
                    this.storageApi + '/api/storage/image/upload-img',
                    { name: this.filename, file: file }
                )
                    .then(response => {
                        this.$notify({
                            title: '成功',
                            message: '新增成功',
                            type: 'success',
                            duration: 2000
                        });
                    }).catch(() => {
                    })
            )
        },
        handleSuccess(response, file, fileList) {
            if (!this.a3Id)
                return;
            let item = {
                A3Id: this.a3Id,
                Type: 'Image',
                category: this.category,
                Name: response.realName,
                Url: this.storageApi + response.url
            }
            this.$axios.posts('api/AAA/A3Attachment/data-post', item).then(response => {
                this.$notify({
                    title: '成功',
                    message: '新增成功',
                    type: 'success',
                    duration: 2000
                });
                this.getAttachment();
            }).catch(() => {
            });
        },
        handlePreview(file) {
            this.dialogImageUrl = file.url;
            this.dialogVisible = true;

        },

        getAttachment() {
            let query = {
                a3Id: this.a3Id,
                category: this.category,
                type: 'Image'

            }
            this.$axios.gets('api/AAA/A3Attachment', query).then(response => {
                this.images = response.items;
            });
        },
    }
}
</script>
