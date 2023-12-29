<template>
  <div class="app-container">
    <div class="head-container">
      <!-- 搜索 -->
      <el-input v-model="listQuery.Filter" clearable size="small" placeholder="搜索..." style="width: 200px;"
        class="filter-item" @keyup.enter.native="handleFilter" />
      <el-button class="filter-item" size="mini" type="success" icon="el-icon-search" @click="handleFilter">搜索
      </el-button>
      <el-button class="filter-item" size="mini" type="primary" icon="el-icon-plus" @click="handleCreate">新增
      </el-button>
      <el-button class="filter-item" size="mini" type="success" icon="el-icon-edit" @click="handleUpdate()">修改
      </el-button>
      <el-button slot="reference" class="filter-item" type="danger" icon="el-icon-delete" size="mini"
        @click="handleDelete()">删除</el-button>
    </div>
    <el-dialog :visible.sync="dialogFormVisible" :close-on-click-modal="false" @close="cancel()" :title="formTitle">
      <el-form ref="form" :model="form" :rules="rules" size="medium" label-width="100px">
        <el-form-item label="activities" prop="name">
          <el-input v-model="form.name" placeholder="请输入name" clearable :style="{ width: '100%' }"></el-input>
        </el-form-item>
        <el-form-item label="responsibility" prop="responsibility">
          <user-select v-model="form.responsibility"></user-select>
          <!-- <el-input v-model="form.responsibility" placeholder="请输入responsibility" clearable
            :style="{ width: '100%' }"></el-input> -->
        </el-form-item>

        <el-form-item label="type" prop="type">
          <el-select v-model="form.type" placeholder="请选择type" clearable :style="{ width: '100%' }">
            <el-option v-for="item in actionTypes" :key="item.id" :value="item.value" :label="item.label"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="status" prop="status">
          <el-select v-model="form.status" placeholder="请选择status" clearable :style="{ width: '100%' }">
            <el-option v-for="item in status" :key="item.id" :value="item.value" :label="item.label"></el-option>
          </el-select>
        </el-form-item>
      </el-form>
      <div slot="footer">
        <el-button size="small" type="text" @click="cancel">取消</el-button>
        <el-button size="small" v-loading="formLoading" type="primary" @click="save">确认</el-button>
      </div>
    </el-dialog>
    <el-table ref="multipleTable" v-loading="listLoading" :data="list" size="small" style="width: 90%;"
      @sort-change="sortChange" @selection-change="handleSelectionChange" @row-click="handleRowClick">
      <el-table-column type="selection" width="44px"></el-table-column>
      <el-table-column label="activities" prop="name" align="center" />
      <el-table-column label="responsibility" prop="responsibility" align="center" />
      <el-table-column label="type" prop="type" align="center" :formatter="typeFormate" />
      <el-table-column label="status" prop="status" align="center" :formatter="statusFormate" />
      <el-table-column label="操作" align="center">
        <template slot-scope="{row}">
          <el-button type="primary" size="mini" @click="handleUpdate(row)" icon="el-icon-edit" />
          <el-button type="danger" size="mini" @click="handleDelete(row)" icon="el-icon-delete" />
        </template>
      </el-table-column>
    </el-table>
    <pagination v-show="totalCount > 0" :total="totalCount" :page.sync="page" :limit.sync="listQuery.MaxResultCount"
      @pagination="getList" />
  </div>
</template>
<script>
import Pagination from "@/components/Pagination";
import permission from "@/directive/permission/index.js";

import UserSelect from '@/views/components/UserSelect'

import baseService from '@/api/base'

const defaultForm = {
  id: null,
  a3Id: null,
  name: null,
  responsibility: null,
  type: null,
  status: null,
}
export default {
  name: 'ContainmentAction',
  components: {
    Pagination,
    UserSelect
  },
  directives: {
    permission
  },
  props: [],
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
      list: null,
      totalCount: 0,
      listLoading: false,
      formLoading: false,
      listQuery: {
        a3Id: '',
        Filter: '',
        Sorting: '',
        SkipCount: 0,
        MaxResultCount: 10
      },
      page: 1,
      dialogFormVisible: false,
      multipleSelection: [],
      formTitle: '',
      isEdit: false,
      actionTypes: [],
      status: [],
    }
  },
  computed: {},
  watch: {},
  created() {
    // this.getList()
    this.getActionTypes();
    this.getStatus();
  },
  mounted() { },
  methods: {
    typeFormate(row, column, val, index) {
      let temp = this.actionTypes.find(item => item.value === val);
      // console.log(temp);
      return temp ? temp.label : 'undefined';
    },
    statusFormate(row, column, val, index) {
      let temp = this.status.find(item => item.value === val);
      // console.log(temp);
      return temp ? temp.label : 'undefined';
    },
    getActionTypes() {
      baseService.fetchOptionsList({ name: 'actionType' }).then(
        res => {
          this.actionTypes = res.data.items;
        }
      )
    },
    getStatus() {
      baseService.fetchOptionsList({ name: 'status' }).then(
        res => {
          this.status = res.data.items;
        })
    },

    getList() {
      this.listLoading = true;
      this.listQuery.SkipCount = (this.page - 1) * this.listQuery.MaxResultCount;
      this.$axios.gets('api/AAA/containmentAction', this.listQuery).then(response => {
        this.list = response.items;
        this.totalCount = response.totalCount;
        this.listLoading = false;
      });
    },
    fetchData(id) {
      this.$axios.gets('api/AAA/containmentAction/' + id).then(response => {
        this.form = response;
      });
    },
    handleFilter() {
      this.page = 1;
      this.getList();
    },
    handleCreate() {
      this.formTitle = '新增表单';
      this.form.a3Id = this.listQuery.a3Id;
      this.isEdit = false;
      this.dialogFormVisible = true;
      this.getActionTypes();
      this.getStatus();
    },
    handleDelete(row) {
      var params = [];
      let alert = '';
      if (row) {
        params.push(row.id);
        alert = row.name;
      }
      else {
        if (this.multipleSelection.length === 0) {
          this.$message({
            message: '未选择',
            type: 'warning'
          });
          return;
        }
        this.multipleSelection.forEach(element => {
          let id = element.id;
          params.push(id);
        });
        alert = '选中项';
      }
      this.$confirm('是否删除' + alert + '?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        this.$axios.posts('api/AAA/containmentAction/delete', params).then(response => {
          this.$notify({
            title: '成功',
            message: '删除成功',
            type: 'success',
            duration: 2000
          });
          this.getList();
        });
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '已取消删除'
        });
      });
    },
    handleUpdate(row) {
      this.formTitle = '修改表单';
      this.isEdit = true;
      if (row) {
        this.fetchData(row.id);
        this.dialogFormVisible = true;
      }
      else {
        if (this.multipleSelection.length != 1) {
          this.$message({
            message: '编辑必须选择单行',
            type: 'warning'
          });
          return;
        }
        else {
          this.fetchData(this.multipleSelection[0].id);
          this.dialogFormVisible = true;
        }
      }
    },
    save() {
      this.$refs.form.validate(valid => {
        if (valid) {
          this.formLoading = true;
          this.form.roleNames = this.checkedRole;
          if (this.isEdit) {
            this.$axios.posts('api/AAA/containmentAction/data-post', this.form).then(response => {
              this.formLoading = false;
              this.$notify({
                title: '成功',
                message: '更新成功',
                type: 'success',
                duration: 2000
              });
              this.dialogFormVisible = false;
              this.getList();
            }).catch(() => {
              this.formLoading = false;
            });
          }
          else {
            this.$axios.posts('api/AAA/containmentAction/data-post', this.form).then(response => {
              this.formLoading = false;
              this.$notify({
                title: '成功',
                message: '新增成功',
                type: 'success',
                duration: 2000
              });
              this.dialogFormVisible = false;
              this.getList();
            }).catch(() => {
              this.formLoading = false;
            });
          }
        }
      });
    },
    sortChange(data) {
      const {
        prop,
        order
      } = data;
      if (!prop || !order) {
        this.handleFilter();
        return;
      }
      this.listQuery.Sorting = prop + ' ' + order;
      this.handleFilter();
    },
    handleSelectionChange(val) {
      this.multipleSelection = val;
    },
    handleRowClick(row, column, event) {
      this.$refs.multipleTable.clearSelection();
      this.$refs.multipleTable.toggleRowSelection(row);
    },
    cancel() {
      this.form = Object.assign({}, defaultForm);
      this.dialogFormVisible = false;
      this.$refs.form.clearValidate();
      // this.actionTypes = [];
      // this.status = [];
    },
  }
}

</script>
<style></style>
