<template>
    <el-select v-model="handleValue" placeholder="" :key="propKeyValue" filterable clearable remote :remote-method="userListRemoteMethod"
        v-loadMore="getUserList" @visible-change="handleUserVisibleChange" :style="{ width: '100%' }" v-bind="$attrs"
        v-on="$listeners">
        <el-option v-for="item in users" :key="item.id" :value="item.email" :label="item.email"></el-option>
    </el-select>
</template>

<script>
import baseService from "@/api/base";
export default {
    props: {
        value: {
            type: [String, Number],
            default: ''
        },
        keyValue: {
            type: String,
            default: 'select-single'
        },
    },
    data() {
        return {
            propKeyValue:'',
            users: [],
            userLoadMoreConfig: {
                offset: 0,
                limit: 10,
                Filter: '',
                page: 1,
                total: 0,
            },
        }
    },
    computed: {
        handleValue: {
            get() {
                return this.value
            },
            set(value) {
                this.$emit('input', value)
            }
        }
    },
    watch: {
        keyValue(val) {
            this.propKeyValue = val
        }
    },
    methods: {
        getUserList() {
            if (this.userLoadMoreConfig.page == 1 || this.userLoadMoreConfig.total - (this.userLoadMoreConfig.page - 1) * this.userLoadMoreConfig.limit > 0) {
                this.userLoadMoreConfig.offset = (this.userLoadMoreConfig.page - 1) * this.userLoadMoreConfig.limit;
                baseService.fetchUserList(this.userLoadMoreConfig).then(
                    res => {
                        let temp = res.data.items;
                        this.userLoadMoreConfig.total = res.data.totalCount;

                        this.users = this.userLoadMoreConfig.page == 1 ? temp : [...this.users, ...temp];
                        this.userLoadMoreConfig.page++;
                    }
                )
            }
        },
        userListRemoteMethod(inputValue) {
            if (inputValue && inputValue.length > 0) {
                this.userLoadMoreConfig.page = 1;
                this.users = [];
                this.userLoadMoreConfig.Filter = inputValue;
                this.getUserList();
            } else {
                this.userLoadMoreConfig.Filter = '';
                this.users = [];
            }
        },
        handleUserVisibleChange(val) {
            if (!val) {
                this.userLoadMoreConfig.Filter = '';
                this.userLoadMoreConfig.page = 1;
                this.users = [];

            }
            else {
                this.getUserList();
            }

        },
    }
}
</script>

<style></style>