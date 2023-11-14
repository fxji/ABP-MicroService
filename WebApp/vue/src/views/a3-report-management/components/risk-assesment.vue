<template>
    <el-form ref="form" :model="form" :rules="rules" size="medium" label-width="100px">
        <el-form-item label="因素" prop="name">
            <el-input v-model="form.name" placeholder="请输入因素" clearable :style="{ width: '100%' }"></el-input>
        </el-form-item>
        <el-form-item label="safetyRelevant" prop="safetyRelevant">
            <el-switch v-model="form.safetyRelevant" active-text="safe" inactive-text="unsafe"></el-switch>
        </el-form-item>
        <el-form-item label="description" prop="description">
            <el-input v-model="form.description" placeholder="请输入description" :style="{ width: '100%' }">
            </el-input>
        </el-form-item>
        <el-form-item label="functionally" prop="functionally">
            <el-select v-model="form.functionally" placeholder="请选择functionally" clearable :style="{ width: '100%' }">
                <el-option v-for="item in levelOptions" :key="item.id" :label="item.label" :value="item.id"></el-option>
            </el-select>
        </el-form-item>
        <el-form-item label="probability" prop="probability">
            <el-select v-model="form.probability" placeholder="请选择probability" clearable :style="{ width: '100%' }">
                <el-option v-for="item in levelOptions" :key="item.id" :label="item.label" :value="item.id"></el-option>
            </el-select>
        </el-form-item>
    </el-form>
</template>

<script>
const defaultForm = {
    id: null,
    name: null,
    safetyRelevant: null,
    description: null,
    functionally: null,
    probability: null,
}
export default {
    name: 'RiskAssessmentPlugin',
    components: {},
    props: [],
    data() {
        return {
            rules: {
                name: [{
                    required: true,
                    message: '请输入因素',
                    trigger: 'blur'
                }],
                safetyRelevant: [{
                    required: true,
                    message: '请输入safetyRelevant',
                    trigger: 'blur'
                }],
                description: [],
                functionally: [],
                probability: [],
            },
            form: Object.assign({}, defaultForm),
        }
    }


}
</script>

<style></style>