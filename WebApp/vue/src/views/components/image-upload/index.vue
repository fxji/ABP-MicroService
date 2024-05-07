<template>
    <el-upload ref="IssueUpload" :file-list="images"
        :action="storageApi + '/api/storage/image/upload-img?name=' + Date.now()" :on-success="handleSuccess"
        list-type="picture-card">
        <i class="el-icon-plus"></i>
    </el-upload>
</template>

<script>
import config from "../../../../static/config";
export default {
    name: 'ImageUpload',
    props: {
        type: {
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

        }
    },
    created() {
    },
    methods: {
        handleSuccess(response, file, fileList) {
            if (!this.a3Id)
                return;
            let item = {
                A3Id: this.a3Id,
                Type: this.type,
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

        getAttachment() {
            let query = {
                a3Id: this.a3Id,
                Type: this.type,

            }
            this.$axios.gets('api/AAA/A3Attachment', query).then(response => {
                this.images = response.items;
            });
        },
    }
}
</script>


