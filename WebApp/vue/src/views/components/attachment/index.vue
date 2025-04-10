<template>
    <div>
        <template v-for="item in attachments" >
            <el-image v-if="isImage(item)" :key="item.id" style="width: 100px; height: 100px; margin: 10px 16px 0 0 ;"
                :src="storageApi + item.url" fit="fill" :preview-src-list="images"></el-image>
        </template>
        <template v-for="item in attachments">

            <div v-if="!isImage(item)" :key="item.id" style="margin-top: 6px;">
                <el-link :key="item.id" icon="el-icon-document" :href="storageApi + item.url">{{ item.name
                    }}</el-link>
            </div>
        </template>
    </div>
</template>

<script>
import config from "../../../../static/config";
export default {
    name: 'Attachment',
    props: {
        category: {
            type: String,
            // default: 'u did not input type',
            required: true
        },
        Id: {
            type: String,
            // default: 'u did not input id',
            required: true
        },
    },
    watch: {
        Id: {
            handler: function (newVal, oldVal) {
                this.getAttachment();
            },
            immediate: true
        }
    },
    data() {
        return {
            placeholder: "No Content!",
            attachments: [],
            images: [],
            storageApi: config.storage.ip,
        }
    },
    methods: {
        getAttachment() {
            let query = {
                a3Id: this.Id,
                category: this.category,
            }
            this.$axios.gets('api/AAA/A3Attachment', query).then(response => {
                this.attachments = response.items;
                this.attachments.forEach(
                    item => {
                        if (item.type == 'Image') {
                            this.images.push(this.storageApi + item.url);
                        }
                    }
                )
            });

        },
        isImage(item) {

            return item.type === "Image"
        }
    }
}
</script>

<style></style>