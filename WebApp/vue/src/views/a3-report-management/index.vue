<template>
    <div class="app-container">
      <div class="head-container">
        <!-- 搜索 -->
        <el-input v-model="listQuery.Filter" clearable size="small" placeholder="搜索..." style="width: 200px;"
          class="filter-item" @keyup.enter.native="handleFilter" />
        <el-button class="filter-item" size="mini" type="success" icon="el-icon-search" @click="handleFilter">{{
          $t("table.search") }}
        </el-button>
        <el-button class="filter-item" size="mini" type="primary" icon="el-icon-plus" @click="handleCreate">新增
        </el-button>
        <el-button class="filter-item" size="mini" type="success" icon="el-icon-edit" @click="handleUpdate()">修改
        </el-button>
        <el-button slot="reference" class="filter-item" type="danger" icon="el-icon-delete" size="mini"
          @click="handleDelete()">删除</el-button>
      </div>
      <el-dialog :visible.sync="dialogFormVisible" :close-on-click-modal="false" @close="cancel()" :title="formTitle">
       <A3Report></A3Report>
        <div slot="footer">
          <el-button size="small" type="text" @click="cancel">取消</el-button>
          <el-button size="small" v-loading="formLoading" type="primary" @click="save">确认</el-button>
        </div>
      </el-dialog>
      <el-table ref="multipleTable" v-loading="listLoading" :data="list" size="small" style="width: 90%;"
        @sort-change="sortChange" @selection-change="handleSelectionChange" @row-click="handleRowClick">
        <el-table-column type="selection" width="44px"></el-table-column>
        <el-table-column label="Title" prop="name" align="center" />
        <el-table-column label="Location/Plant/Site" prop="organizationId" align="center" />
        <el-table-column label="Sponsor / Champion" prop="userId" align="center" />
        <el-table-column label="开关" prop="reOccurrence" align="center" />
        <el-table-column label="Process of Production issue" prop="processId" align="center" />
        <el-table-column label="Reference A3 " prop="parentId" align="center" />
        <el-table-column label="Source Of Defect" prop="sourceId" align="center" />
        <el-table-column label="日期选择" prop="StartDate" align="center" />
        <el-table-column label="操作" align="center">
          <template slot-scope="{ row }">
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
  import A3Report from '@/views/a3-report-management/components/a3-report'
  import Pagination from "@/components/Pagination";
  import TreeSelect from "@riophae/vue-treeselect";
  import '@riophae/vue-treeselect/dist/vue-treeselect.css'
  import { LOAD_CHILDREN_OPTIONS } from "@riophae/vue-treeselect";
  
  import permission from "@/directive/permission/index.js";
  
  import baseService from "@/api/base";
  import a3Service from "@/api/aaa";
  
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
    name: "A3ReportManagement",
    components: {
      Pagination,
      TreeSelect,
      A3Report
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
        list: null,
        processList: [],
        defectSources: [],
        totalCount: 0,
        listLoading: true,
        formLoading: false,
        listQuery: {
          Filter: "",
          Sorting: "",
          SkipCount: 0,
          MaxResultCount: 10
        },
        page: 1,
        dialogFormVisible: false,
        multipleSelection: [],
        formTitle: "",
        isEdit: false
      };
    },
    computed: {},
    watch: {},
    created() {
      this.getList();
    },
    mounted() { },
    methods: {
      getList() {
        this.listLoading = true;
        this.listQuery.SkipCount = (this.page - 1) * this.listQuery.MaxResultCount;
        a3Service.fetchList(this.listQuery).then(response => {
          this.list = response.data.items;
          this.totalCount = response.data.totalCount;
          this.listLoading = false;
        });
      },
      getProcessList() {
        baseService.fetchOptionsList({ name: 'process' }).then(
          res => {
            this.processList = res.data.items;
          }
        )
      },
      getDefectSource() {
        baseService.fetchOptionsList({ name: 'defectSource' }).then(
          res => {
            this.defectSources = res.data.items;
          }
        )
      },
      getOrgNodes() {
        baseService.fetchOrgNodesList().then((response) => {
          this.loadTree(response.data);
        });
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
      fetchData(id) {
        this.getOrgNodes();
  
        a3Service.fetchSingle(id).then(response => {
          this.form = response.data;
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
      handleFilter() {
        this.page = 1;
        this.getList();
      },
      handleCreate() {
        this.formTitle = "新增表单";
        this.isEdit = false;
        this.dialogFormVisible = true;
        this.getOrgNodes();
        this.getProcessList();
        this.getDefectSource();
      },
      handleDelete(row) {
        var params = [];
        let alert = "";
        if (row) {
          params.push(row.id);
          alert = row.name;
        } else {
          if (this.multipleSelection.length === 0) {
            this.$message({
              message: "未选择",
              type: "warning"
            });
            return;
          }
          this.multipleSelection.forEach(element => {
            let id = element.id;
            params.push(id);
          });
          alert = "选中项";
        }
        this.$confirm("是否删除" + alert + "?", "提示", {
          confirmButtonText: "确定",
          cancelButtonText: "取消",
          type: "warning"
        })
          .then(() => {
            this.$axios.posts("api/AAA/a3/delete", params).then(response => {
              this.$notify({
                title: "成功",
                message: "删除成功",
                type: "success",
                duration: 2000
              });
              this.getList();
            });
          })
          .catch(() => {
            this.$message({
              type: "info",
              message: "已取消删除"
            });
          });
      },
      handleUpdate(row) {
        this.formTitle = "修改表单";
        this.isEdit = true;
        if (row) {
          this.fetchData(row.id);
          this.dialogFormVisible = true;
        } else {
          if (this.multipleSelection.length != 1) {
            this.$message({
              message: "编辑必须选择单行",
              type: "warning"
            });
            return;
          } else {
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
              a3Service.post(this.form)
                .then(response => {
                  this.formLoading = false;
                  this.$notify({
                    title: "成功",
                    message: "更新成功",
                    type: "success",
                    duration: 2000
                  });
                  this.dialogFormVisible = false;
                  this.getList();
                })
                .catch(() => {
                  this.formLoading = false;
                });
            } else {
              a3Service.post(this.form)
                .then(response => {
                  this.formLoading = false;
                  this.$notify({
                    title: "成功",
                    message: "新增成功",
                    type: "success",
                    duration: 2000
                  });
                  this.dialogFormVisible = false;
                  this.getList();
                })
                .catch(() => {
                  this.formLoading = false;
                });
            }
          }
        });
      },
      sortChange(data) {
        const { prop, order } = data;
        if (!prop || !order) {
          this.handleFilter();
          return;
        }
        this.listQuery.Sorting = prop + " " + order;
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
        this.orgs = [];
        this.processList = [];
        this.defectSources = [];
        this.dialogFormVisible = false;
        this.$refs.form.clearValidate();
      }
    }
  };
  </script>
  <style></style>
  