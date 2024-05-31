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
      <el-form ref="form" :model="form" :rules="rules" size="medium" label-width="150px">
        <el-form-item label="Factor" prop="name">
          <el-input v-model="form.name" placeholder="请输入Factor" clearable :style="{ width: '100%' }"></el-input>
        </el-form-item>
        <el-form-item label="SafetyRelevant" prop="safetyRelevant">
          <el-switch v-model="form.safetyRelevant"></el-switch>
        </el-form-item>
        <el-form-item label="Description" prop="description">
          <el-input v-model="form.description" placeholder="请输入description" type="textarea" :style="{ width: '100%' }">
          </el-input>
        </el-form-item>
        <el-form-item label="Functionally" prop="functionally">
          <el-select v-model="form.functionally" placeholder="请选择functionally" clearable :style="{ width: '100%' }">
            <el-option v-for="item in levelOptions" :key="item.id" :label="item.label" :value="item.value"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="Probability" prop="probability">
          <el-select v-model="form.probability" placeholder="请选择probability" clearable :style="{ width: '100%' }">
            <el-option v-for="item in levelOptions" :key="item.id" :label="item.label" :value="item.value"></el-option>
          </el-select>
        </el-form-item>

      </el-form>
      <div slot="footer">
        <el-button size="small" type="text" @click="cancel">取消</el-button>
        <el-button size="small" v-loading="formLoading" type="primary" @click="save">确认</el-button>
      </div>
    </el-dialog>

    <el-row>
      <el-col :xs="14" :sm="15" :md="15" :lg="16" :xl="16">

        <el-table ref="multipleTable" v-loading="listLoading" :data="list" size="small" style="width: 90%;"
          @sort-change="sortChange" @selection-change="handleSelectionChange" @row-click="handleRowClick">
          <el-table-column type="selection" width="44px"></el-table-column>
          <el-table-column label="Factor" prop="name" align="center" />
          <el-table-column label="SafetyRelevant" prop="safetyRelevant" align="center">
            <template slot-scope="scope">
              <el-switch :disabled=true v-model="scope.row.safetyRelevant" active-color="Green" />
            </template>
          </el-table-column>
          <el-table-column label="Functionally" :formatter="levelFormate" prop="functionally" align="center" />
          <el-table-column label="Probability" :formatter="levelFormate" prop="probability" align="center" />
          <!-- <el-table-column label="description" prop="description" align="center" /> -->
          <!-- <el-table-column label="操作" align="center">
            <template slot-scope="{row}">
              <el-button type="primary" size="mini" @click="handleUpdate(row)" icon="el-icon-edit" />
              <el-button type="danger" size="mini" @click="handleDelete(row)" icon="el-icon-delete" />
            </template>
          </el-table-column> -->
        </el-table>
      </el-col>
      <el-col :xs="10" :sm="9" :md="9" :lg="8" :xl="8">
        <el-row style="margin-top: 10px;">
          <image-upload :category="attachmentTypes.RiskAssesment" :a3Id="a3Id"></image-upload>
        </el-row>
        <el-row style="margin-top: 10px;">
          <docs-upload :category="attachmentTypes.RiskAssesment" :a3Id="a3Id"></docs-upload>
        </el-row>
      </el-col>
    </el-row>
    <pagination v-show="totalCount > 10" :total="totalCount" :page.sync="page" :limit.sync="listQuery.MaxResultCount"
      @pagination="getList" />
  </div>
</template>
<script>

import ImageUpload from '@/views/components/image-upload'
import DocsUpload from '@/views/components/docs-upload'

import Pagination from "@/components/Pagination";
import permission from "@/directive/permission/index.js";

import baseService from '@/api/base'

const defaultForm = {
  id: null,
  a3Id: null,
  name: null,
  safetyRelevant: null,
  description: null,
  functionally: null,
  probability: null,
}
export default {
  name: 'RiskAssessment',
  components: {
    Pagination,
    ImageUpload,
    DocsUpload
  },
  directives: {
    permission
  },
  props: {},
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
      levelOptions: [],

      attachmentTypes: {
        ContainmentAction: 'ContainmentAction',
        RiskAssesment: 'RiskAssesment',
        Issue: 'Issue',
        Cause: 'Cause',
        CorrectiveAction: 'CorrectiveAction',
      },

    }
  },
  computed: {},
  props: [
    'a3Id'
  ],
  watch: {
    a3Id: {
      handler: function (newVal, oldVal) {
        this.form.a3Id = newVal;
        this.listQuery.a3Id = newVal;
        this.getList();
      },
      immediate: true
    }
  },
  created() {
    // this.getList()
    this.getLevelOptions();
  },
  mounted() { },
  methods: {
    levelFormate(row, column, val, index) {
      let temp = this.levelOptions.find(item => item.value === val);
      // console.log(temp);
      return temp ? temp.label : 'undefined';
    },
    getLevelOptions() {
      // if (Object.keys(levelOptions).length > 0) {
      //   return;
      // }
      //减少访问后台次数
      if (this.levelOptions.length > 0) {
        return;
      }
      baseService.fetchOptionsList({ name: 'levels' }).then(
        res => {
          this.levelOptions = res.data.items;
        }
      )
    },
    getList() {
      this.listLoading = true;
      this.listQuery.SkipCount = (this.page - 1) * this.listQuery.MaxResultCount;
      this.$axios.gets('api/AAA/riskAssessment', this.listQuery).then(response => {
        this.list = response.items;
        this.totalCount = response.totalCount;
        this.listLoading = false;
      });
    },
    fetchData(id) {
      this.$axios.gets('api/AAA/riskAssessment/' + id).then(response => {
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
      this.getLevelOptions();
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
        this.$axios.posts('api/AAA/riskAssessment/delete', params).then(response => {
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
            this.$axios.posts('api/AAA/riskAssessment/data-post', this.form).then(response => {
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
            this.$axios.posts('api/AAA/riskAssessment/data-post', this.form).then(response => {
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
    },
  }
}

</script>
<style></style>
