<template>
    <el-upload ref="IssueUpload" :file-list="docs" :action="storageApi + '/api/storage/doc/upload-doc?name=' + Date.now()"
        :on-success="handleSuccess" list-type="text">
        <el-button size="small" type="primary">上传文档</el-button>
    </el-upload>
</template>

<script>
import config from "../../../../static/config";
export default {
    name: 'DocsUpload',
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
            docs: [],

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
                category: this.category,
                type:'Doc',
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
                category: this.category,
                type:'Doc'

            }
            this.$axios.gets('api/AAA/A3Attachment', query).then(response => {
                this.docs = response.items;
            });
        },
    }
}
</script>


