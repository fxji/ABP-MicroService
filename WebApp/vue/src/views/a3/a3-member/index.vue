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
      <el-form ref="form" :model="form" :rules="rules" size="medium" label-width="120px">
        <el-form-item label="Member" prop="userId">
          <user-select v-model="form.userId"></user-select>
          <!-- <el-select v-model="form.userId" placeholder="请选择Member" clearable :style="{width: '100%'}">
          </el-select> -->
        </el-form-item>
        <el-form-item label="Department" prop="organizationId">
          <tree-select :multiple="false" v-model="form.organizationId" :load-options="loadOrgs" :options="orgs"
            placeholder="请选择Location/Plant/Site" />
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
      <el-table-column label="Member" prop="userId" align="center" />
      <el-table-column label="Organization" prop="organizationId" :formatter="locationFormatter" align="center" />
      <!-- <el-table-column label="操作" align="center">
        <template slot-scope="{row}">
          <el-button type="primary" size="mini" @click="handleUpdate(row)" icon="el-icon-edit" />
          <el-button type="danger" size="mini" @click="handleDelete(row)" icon="el-icon-delete" />
        </template>
      </el-table-column> -->
    </el-table>
    <pagination v-show="totalCount > 10" :total="totalCount" :page.sync="page" :limit.sync="listQuery.MaxResultCount"
      @pagination="getList" />
  </div>
</template>
<script>

import UserSelect from '@/views/components/user-select'
import TreeSelect from "@riophae/vue-treeselect";

import Pagination from "@/components/Pagination";
import permission from "@/directive/permission/index.js";

import a3Service from "@/api/aaa";
import baseService from "@/api/base";

const defaultForm = {
  id: null,
  a3Id: null,
  userId: null,
  organizationId: null,
}
export default {
  name: 'A3Member',
  components: {
    Pagination,
    UserSelect,
    TreeSelect
  },
  directives: {
    permission
  },
  data() {
    return {
      rules: {
        userId: [{
          required: true,
          message: '请选择Member',
          trigger: 'change'
        }],
        organizationId: [{
          required: true,
          message: '请选择Organization',
          trigger: 'change'
        }],
      },
      orgs: [],
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
      noTreeOrgs: [],
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
    this.getOrgNodes();

  },
  mounted() { },
  methods: {
    getList() {
      this.listLoading = true;
      this.listQuery.SkipCount = (this.page - 1) * this.listQuery.MaxResultCount;
      this.$axios.gets('api/aaa/A3member', this.listQuery).then(response => {
        this.list = response.items;
        this.totalCount = response.totalCount;
        this.listLoading = false;
      });
    },
    fetchData(id) {
      this.getOrgNodes();
      this.$axios.gets('api/aaa/A3member/' + id).then(response => {
        this.form = response;
      });
    },
    handleFilter() {
      this.page = 1;
      this.getList();
    },
    handleCreate() {
      this.formTitle = '新增A3Member';
      this.form.a3Id = this.listQuery.a3Id;
      this.isEdit = false;
      this.getOrgNodes();
      this.dialogFormVisible = true;
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
        this.$axios.posts('api/aaa/A3member/delete', params).then(response => {
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
      this.formTitle = '修改A3Member';
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
    locationFormatter(row, column, val, index) {
      let temp = this.noTreeOrgs.find(item => item.id === val);
      return temp ? temp.label : undefined;
    },
    save() {
      this.$refs.form.validate(valid => {
        if (valid) {
          this.formLoading = true;
          this.form.roleNames = this.checkedRole;
          if (this.isEdit) {
            this.$axios.posts('api/aaa/A3member/data-post', this.form).then(response => {
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
            this.$axios.posts('api/aaa/A3member/data-post', this.form).then(response => {
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
      this.orgs = [];
    },
    //TODO：引用公共方法
    loadTree(data) {
      data.items.forEach((element) => {
        if (!element.pid) {
          element.hasChildren = element.leaf ? false : true;
          if (!element.leaf) {
            element.children = [];
          }
          this.orgs.push(element);
        }
      });
      this.setChildren(this.orgs, data.items);
    },
    setChildren(roots, items) {
      roots.forEach((element) => {
        items.forEach((item) => {
          if (item.pid == element.id) {
            if (!element.children) element.children = [];
            element.children.push(item);
          }
        });
        if (element.children) {
          this.setChildren(element.children, items);
        }
      });
    },
    getOrgNodes() {
      if (this.orgs.length > 0) {
        return;
      }
      baseService.fetchOrgNodesList().then((response) => {
        this.noTreeOrgs = response.items;
        this.loadTree(response);
      });
    },

    loadOrgs({ action, parentNode, callback }) {
      if (action === LOAD_CHILDREN_OPTIONS) {
        fetchOrgs(parentNode.id)
          .then((response) => {
            parentNode.children = response.items.map(function (obj) {
              if (!obj.leaf) {
                obj.children = null;
              }
              return obj;
            });
            setTimeout(() => {
              callback();
            }, 100);
          });
      }
    },
  }
}

</script>
<style></style>
