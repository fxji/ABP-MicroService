<template>
    <el-form ref="form" :model="form" :rules="rules" size="medium" label-width="200px">
        <el-form-item label="Title" prop="name">
            <el-tooltip content="description of the issue">
                <el-input v-model="form.name" placeholder="请输入Title" clearable :style="{ width: '100%' }"></el-input>
            </el-tooltip>
        </el-form-item>
        <el-form-item label="Plant" prop="organizationId">
            <tree-select :multiple="false" v-model="form.organizationId" :load-options="loadOrgs" :options="orgs"
                placeholder="请选择Location/Plant/Site" />
        </el-form-item>
        <el-form-item label="Sponsor / Champion" prop="userId">
            <el-select v-model="form.userId" placeholder="请选择Sponsor / Champion" clearable
                :style="{ width: '100%' }"></el-select>
        </el-form-item>
        <el-form-item label="reOccurrence" prop="reOccurrence">
            <el-switch v-model="form.reOccurrence" :active-value="undefined" :inactive-value="undefined">
            </el-switch>
        </el-form-item>
        <el-form-item label="Process" prop="processId">
            <el-select v-model="form.processId" placeholder="请选择Process of Production issue" clearable
                :style="{ width: '100%' }">
                <el-option v-for="item in processList" :key="item.id" :label="item.label" :value="item.id"></el-option>

            </el-select>
        </el-form-item>
        <el-form-item label="Reference A3 " prop="parentId">
            <el-select v-model="form.parentId" placeholder="请选择Reference A3 " clearable :style="{ width: '100%' }">

            </el-select>
        </el-form-item>
        <el-form-item label="Source Of Defect" prop="sourceId">
            <el-select v-model="form.sourceId" placeholder="请选择Source Of Defect" clearable :style="{ width: '100%' }">
                <el-option v-for="item in defectSources" :key="item.id" :label="item.label" :value="item.id">

                </el-option>
            </el-select>
        </el-form-item>
        <el-form-item label="日期选择" prop="StartDate">
            <el-date-picker type="date" v-model="form.StartDate" :style="{ width: '100%' }" placeholder="请选择日期选择"
                clearable></el-date-picker>
        </el-form-item>
    </el-form>
</template>

<script>

import TreeSelect from "@riophae/vue-treeselect";
import '@riophae/vue-treeselect/dist/vue-treeselect.css'
import { LOAD_CHILDREN_OPTIONS } from "@riophae/vue-treeselect";

import permission from "@/directive/permission/index.js";
const defaultForm = {
    id: null,
    name: null,
    organizationId: null,
    userId: null,
    reOccurrence: false,
    processId: null,
    parentId: null,
    sourceId: null,
    StartDate: null
};
export default {
    name: "A3BasicInfo",
    components: {
        TreeSelect
    },
    directives: {
        permission
    },
    props: [],
    data() {
        return {
            rules: {
                name: [
                    {
                        required: true,
                        message: "请输入Title",
                        trigger: "blur"
                    }
                ],
                organizationId: [],
                userId: [],
                processId: [],
                parentId: [],
                sourceId: [],
                StartDate: []
            },
            orgs: [],
            form: Object.assign({}, defaultForm),
        }
    }
}
</script>

<style></style>