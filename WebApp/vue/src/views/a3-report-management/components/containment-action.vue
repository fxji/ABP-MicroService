<template>
    <el-form ref="form" :model="form" :rules="rules" size="medium" label-width="100px">
        <el-form-item label="activities" prop="name">
            <el-input v-model="form.name" placeholder="请输入name" clearable :style="{ width: '100%' }"></el-input>
        </el-form-item>
        <el-form-item label="responsibility" prop="responsibility">
            <el-input v-model="form.responsibility" placeholder="请输入responsibility" clearable
                :style="{ width: '100%' }"></el-input>
        </el-form-item>

        <el-form-item label="type" prop="type">
            <el-select v-model="form.type" placeholder="请选择type" clearable :style="{ width: '100%' }">
                <el-option v-for="item in actionTypes" :key="item.id" :value="item.id" :label="item.label"></el-option>
            </el-select>
        </el-form-item>
        <el-form-item label="status" prop="status">
            <el-select v-model="form.status" placeholder="请选择status" clearable :style="{ width: '100%' }">
                <el-option v-for="item in status" :key="item.id" :value="item.id" :label="item.label"></el-option>
            </el-select>
        </el-form-item>
    </el-form>
</template>

<script>
const defaultForm = {
    id: null,
    name: null,
    responsibility: null,
    type: null,
    status: null,
}
export default {
    name: 'ContainmentActionPlugin',
    components: {},
    props: {

    },
    data() {

        return {
            rules: {
                name: [{
                    required: true,
                    message: '请输入name',
                    trigger: 'blur'
                }],
                responsibility: [],
                type: [],
                status: [],
            },
            form: Object.assign({}, defaultForm),
        }
    },
}
</script>

<style></style>