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
        <el-form-item label="Title" prop="name">
          <el-tooltip content="description of the issue">
            <el-input v-model="form.name" placeholder="请输入Title" clearable :style="{ width: '100%' }"></el-input>
          </el-tooltip>
        </el-form-item>
        <el-form-item label="Problem Type" prop="type">
          <el-select v-model="form.type" placeholder="请选择Problem Type" clearable @visible-change="handleTypeVisibleChange"
            :style="{ width: '100%' }">
            <el-option v-for="item in issueTypes" :key="item.id" :value="item.value" :label="item.label"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="Customer Group" prop="customerGroup">
          <el-select v-model="form.customerGroup" placeholder="请选择Customer" filterable clearable remote
            :remote-method="customerListRemoteMethod" v-loadMore="getCustomerList"
            @visible-change="handleCustomerVisibleChange" :style="{ width: '100%' }">
            <el-option v-for="item in customers" :key="item.id" :value="item.name" :label="item.name"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="Project Name" prop="project">
          <el-select v-model="form.project" placeholder="请选择Product" filterable clearable remote
            :remote-method="projectListRemoteMethod" v-loadMore="getProjectList"
            @visible-change="handleProductVisibleChange" :style="{ width: '100%' }">
            <el-option v-for="item in projects" :key="item.id" :label="item.ProjectName"
              :value="item.ProjectName"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="Failure Qty/Rate" prop="rate">
          <el-input-number v-model="form.rate" :precision=2 placeholder="Failure Qty/Rate"></el-input-number>
        </el-form-item>
        <el-form-item label="Occurrence Date" prop="occurrenceDate">
          <el-date-picker v-model="form.occurrenceDate" :style="{ width: '100%' }" placeholder="请选择时间选择"
            clearable></el-date-picker>
        </el-form-item>
        <el-form-item label="Description" prop="description">
          <el-input v-model="form.description" placeholder="请输入Symptom Description" type="textarea"
            :style="{ width: '100%' }">
          </el-input>
        </el-form-item>
        <el-form-item label="Current Situation" prop="symptomDescription">
          <el-input v-model="form.symptomDescription" placeholder="请输入Symptom Description" type="textarea"
            :style="{ width: '100%' }"></el-input>
        </el-form-item>
        <el-form-item label="Goal Statement" prop="goalStatement">
          <el-input v-model="form.goalStatement" placeholder="请输入Goal Statement" type="textarea"
            :style="{ width: '100%' }">
          </el-input>
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
          <el-table-column label="Title" prop="name" align="center" />
          <el-table-column label="Problem Type" prop="type" align="center" :formatter="issueTypeFormatter" />
          <el-table-column label="Customer Group" prop="customerGroup" align="center" />
          <el-table-column label="Project Name" prop="project" align="center" />
          <el-table-column label="OccurrenceDate" prop="occurrenceDate" align="center" />
          <el-table-column label="Failure Qty/Rate" prop="rate" align="center" />
          <!-- <el-table-column label="Goal Statement" prop="goalStatement" align="center" /> -->
          <!-- <el-table-column label="Description" prop="description" align="center" /> -->
          <!-- <el-table-column label="Current Situation" prop="symptomDescription" align="center" /> -->
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
          <image-upload :type="attachmentTypes.Issue" :a3Id="a3Id"></image-upload>
        </el-row>
        <el-row style="margin-top: 10px;">
          <docs-upload :type="attachmentTypes.IssueDocs" :a3Id="a3Id"></docs-upload>
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
import baseService from "@/api/base";
import lmtService from '@/api/lmt'

const defaultForm = {
  name: null,
  id: null,
  a3Id: null,
  goalStatement: null,
  rate: 0,
  description: null,
  symptomDescription: null,
  type: null,
  customerGroup: null,
  project: null,
  occurrenceDate: null,
}


export default {
  name: 'Issue',
  components: {
    Pagination,
    ImageUpload,
    DocsUpload
  },
  directives: {
    permission
  },
  props: [
    'a3Id'
  ],
  filters: {

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
        customerGroup: [],
        project: [],
        occurrenceDate: [],
      },
      form: Object.assign({}, defaultForm),
      list: null,
      issueTypes: [],
      projects: [],
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
      loadMoreConfig: {
        offset: 0,
        limit: 10,
        search: ''
      },
      loadMorePage: 1,
      loadMoreTotalCount: 0,
      customers: [],
      customerLoadMoreConfig: {
        offset: 0,
        limit: 10,
        search: '',
        page: 1,
        total: 0,
      },
      dialogFormVisible: false,
      multipleSelection: [],
      formTitle: '',
      isEdit: false,
      
      attachmentTypes: {
        ContainmentAction: 'ContainmentAction',
        RiskAssesment: 'RiskAssesment',
        Issue: 'Issue',
        Cause: 'Cause',
        CorrectiveAction: 'CorrectiveAction',
        ContainmentActionDocs: 'ContainmentActionDocs',
        RiskAssesmentDocs: 'RiskAssesmentDocs',
        IssueDocs: 'IssueDocs',
        CauseDocs: 'CauseDocs',
        CorrectiveActionDocs: 'CorrectiveActionDocs',
      },

    }
  },
  computed: {},
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

    this.getIssueTypeList();

  },
  mounted() { },
  methods: {
    getList() {
      this.listLoading = true;
      this.listQuery.SkipCount = (this.page - 1) * this.listQuery.MaxResultCount;
      this.$axios.gets('api/AAA/issue', this.listQuery).then(response => {
        this.list = response.items;
        this.totalCount = response.totalCount;
        this.listLoading = false;
      });
    },
    getIssueTypeList() {
      if (this.issueTypes.length > 0) {
        return;
      }
      baseService.fetchOptionsList({ name: 'issueType' }).then(
        res => {
          this.issueTypes = res.data.items;
        }
      )
    },
    getProjectList() {
      if (this.loadMorePage == 1 || this.loadMoreTotalCount - (this.loadMorePage - 1) * this.loadMoreConfig.limit > 0) {

        this.loadMoreConfig.offset = (this.loadMorePage - 1) * this.loadMoreConfig.limit;

        lmtService.fetchProjectList(this.loadMoreConfig).then(
          res => {
            let temp = res.data.data.results;
            this.loadMoreTotalCount = res.data.data.count;
            //合并新增
            this.projects = this.loadMorePage == 1 ? temp : [...this.projects, ...temp]
            this.loadMorePage++;
          }
        )

      }
    },
    issueTypeFormatter(row, column, val, index) {
      // this.getIssueTypeList();
      let temp = this.issueTypes.find(item => item.value === val);
      // console.log(temp);
      return temp ? temp.label : 'undefined';
      // return this.commonFormatter(this.issueTypes, val);
    },


    projectListRemoteMethod(inputValue) {
      if (inputValue && inputValue.length > 0) {
        this.loadMorePage = 1;
        this.projects = [];
        this.loadMoreConfig.search = inputValue;
        this.getProjectList();
      }
      else {
        this.loadMoreConfig.search = '';
        this.projects = [];
      }
    },

    getCustomerList() {
      if (this.customerLoadMoreConfig.page == 1 || this.customerLoadMoreConfig.total - (this.customerLoadMoreConfig.page - 1) * this.customerLoadMoreConfig.limit > 0) {
        this.customerLoadMoreConfig.offset = (this.customerLoadMoreConfig.page - 1) * this.customerLoadMoreConfig.limit;
        lmtService.fetchCustomerList(this.customerLoadMoreConfig).then(
          res => {
            let temp = res.data.data.results;
            this.customerLoadMoreConfig.total = res.data.data.count;

            this.customers = this.customerLoadMoreConfig.page == 1 ? temp : [...this.customers, ...temp];
            this.customerLoadMoreConfig.page++;
          }
        )
      }
    },
    customerListRemoteMethod(inputValue) {
      if (inputValue && inputValue.length > 0) {
        this.customerLoadMoreConfig.page = 1;
        this.customers = [];
        this.customerLoadMoreConfig.search = inputValue;
        this.getCustomerList();
      } else {
        this.customerLoadMoreConfig.search = '';
        this.customers = [];
      }
    },
    handleCustomerVisibleChange(val) {
      if (!val) {
        this.customerLoadMoreConfig.search = '';
        this.customerLoadMoreConfig.page = 1;
        this.customers = [];

      }
      else {
        this.getCustomerList();
      }

    },
    handleProductVisibleChange(val) {
      if (!val) {
        this.loadMoreConfig.search = '';
        this.loadMorePage = 1;
        this.projects = [];
      }
      else {
        this.getProjectList();
      }

    },
    handleTypeVisibleChange(val) {
      if (!val) {
        this.issueTypes = [];
      }
      else {
        this.getIssueTypeList();
      }
    },
    fetchData(id) {
      this.$axios.gets('api/AAA/issue/' + id).then(response => {
        this.form = response;
      });
    },
    handleFilter() {
      this.page = 1;
      this.getList();
    },
    handleCreate() {
      this.formTitle = '新增问题';
      //from top level click event
      this.form.a3Id = this.listQuery.a3Id;
      this.isEdit = false;
      this.dialogFormVisible = true;

      // setTimeout(() => {

      //   this.getProjectList();
      // }, 800);

      // this.getProjectList();
      this.getIssueTypeList();
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
        this.$axios.posts('api/AAA/issue/delete', params).then(response => {
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
      this.formTitle = '修改问题';
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
            this.$axios.posts('api/AAA/issue/data-post', this.form).then(response => {
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
            this.$axios.posts('api/AAA/issue/data-post', this.form).then(response => {
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
      // this.issueTypes = [];
      // this.projects = [];
      // this.loadMorePage = 1;
      // this.loadMoreTotalCount = 0;
      this.$refs.form.clearValidate();
    },
    

    

  }
}

</script>
<style></style>
