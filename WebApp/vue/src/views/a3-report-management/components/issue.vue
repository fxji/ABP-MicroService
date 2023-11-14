<template>
    <el-form ref="form" :model="form" :rules="rules" size="medium" label-width="100px">
        <el-form-item label="Title" prop="name">
            <el-tooltip content="description of the issue">
                <el-input v-model="form.name" placeholder="请输入Title" clearable :style="{ width: '100%' }"></el-input>
            </el-tooltip>
        </el-form-item>
        <el-form-item label="Problem Type" prop="type">
            <el-select v-model="form.type" placeholder="请选择Problem Type" clearable :style="{ width: '100%' }">
                <el-option v-for="item in issueTypes" :key="item.id" :value="item.id" :label="item.label"></el-option>
            </el-select>
        </el-form-item>
        <el-form-item label="Customer" prop="customerId">
            <el-select v-model="form.customerId" placeholder="请选择Customer" filterable clearable remote
                :remote-method="customerListRemoteMethod" v-loadMore="getCustomerList"
                @visible-change="handleVisitChangeCustomerList" :style="{ width: '100%' }">
                <el-option v-for="item in customers" :key="item.id" :value="item.id" :label="item.name"></el-option>
            </el-select>
        </el-form-item>
        <el-form-item label="Product" prop="productId">
            <el-select v-model="form.productId" placeholder="请选择Product" filterable clearable remote
                :remote-method="projectListRemoteMethod" v-loadMore="getProjectList" @visible-change="handleVisitChangeR"
                :style="{ width: '100%' }">
                <el-option v-for="item in projects" :key="item.id" :label="item.ProjectName" :value="item.id"></el-option>
            </el-select>
        </el-form-item>
        <el-form-item label="Failure Qty/Rate" prop="rate">
            <el-input-number v-model="form.rate" placeholder="Failure Qty/Rate"></el-input-number>
        </el-form-item>
        <el-form-item label="时间选择" prop="occurrenceDate">
            <el-time-picker v-model="form.occurrenceDate" :style="{ width: '100%' }" placeholder="请选择时间选择"
                clearable></el-time-picker>
        </el-form-item>
        <el-form-item label="Description" prop="description">
            <el-input v-model="form.description" placeholder="请输入Symptom Description" :style="{ width: '100%' }">
            </el-input>
        </el-form-item>
        <el-form-item label="Symptom Description" prop="symptomDescription">
            <el-input v-model="form.symptomDescription" placeholder="请输入Symptom Description"
                :style="{ width: '100%' }"></el-input>
        </el-form-item>
        <el-form-item label="Goal Statement" prop="goalStatement">
            <el-input v-model="form.goalStatement" placeholder="请输入Goal Statement" :style="{ width: '100%' }">
            </el-input>
        </el-form-item>
    </el-form>
</template>

<script>
const defaultForm = {
    name: null,
    id: null,
    goalStatement: null,
    rate: 0,
    description: null,
    symptomDescription: null,
    type: null,
    customerId: null,
    productId: null,
    occurrenceDate: null,
}
export default {
    name: 'IssuePlugin',
    components: {},
    props: {

    },
    data() {
        return {
            rules: {
                name: [
                    {
                        required: true,
                        message: "请输入Title",
                        trigger: "blur"
                    }],
                goalStatement: [],
                rate: [],
                description: [],
                symptomDescription: [],
                type: [{
                    required: true,
                    message: '请选择Problem Type',
                    trigger: 'change'
                }],
                customerId: [],
                productId: [],
                occurrenceDate: [],
            },
            form: Object.assign({}, defaultForm),
        }
    }

}
</script>