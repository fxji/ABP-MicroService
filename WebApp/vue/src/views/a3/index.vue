<template>
  <div class="app-container">
    <el-card>
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
        <el-form ref="form" :model="form" :rules="rules" size="medium" label-width="150px">
          <el-form-item label="Title" prop="name">
            <el-tooltip content="description of the issue">
              <el-input v-model="form.name" placeholder="请输入Title" clearable :style="{ width: '100%' }"></el-input>
            </el-tooltip>
          </el-form-item>
          <el-form-item label="Department" prop="organizationId">
            <tree-select :multiple="false" v-model="form.organizationId" :load-options="loadOrgs" :options="orgs"
              placeholder="请选择Location/Plant/Site" />
          </el-form-item>
          <el-form-item label="Sponsor" prop="userEmail">
            <user-select v-model="form.userEmail"></user-select>
            <!-- <el-select v-model="form.userId" placeholder="请选择Sponsor" filterable clearable remote
              :remote-method="userListRemoteMethod" v-loadMore="getUserList" @visible-change="handleUserVisibleChange"
              :style="{ width: '100%' }">
              <el-option v-for="item in users" :key="item.id" :value="item.id" :label="item.email"></el-option>
            </el-select> -->
          </el-form-item>
          <el-form-item label="Re-Occurrence" prop="reOccurrence">
            <el-switch v-model="form.reOccurrence" :active-value="undefined" :inactive-value="undefined">
            </el-switch>
          </el-form-item>
          <el-form-item label="Process" prop="process">
            <el-select v-model="form.process" placeholder="请选择Process of Production issue" clearable
              @visible-change="handleProcessVisibleChange" :style="{ width: '100%' }">
              <el-option v-for="item in processList" :key="item.id" :label="item.label" :value="item.value"></el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="Reference A3 " prop="parentId">
            <!-- TODO remote
              :remote-method="a3ListRemoteMethod" v-loadMore="getA3List"
            @visible-change="handleA3VisibleChange"  
          -->
            <el-select v-model="form.parentId" placeholder="请选择Reference" filterable clearable remote
              :style="{ width: '100%' }">
              <el-option v-for="item in list" :key="item.id" :value="item.id" :label="item.name"></el-option>
            </el-select>

          </el-form-item>
          <el-form-item label="Source Of Defect" prop="source">
            <el-select v-model="form.source" placeholder="请选择Source Of Defect" clearable
              @visible-change="handleDefectSourceVisibleChange" :style="{ width: '100%' }">
              <el-option v-for="item in defectSources" :key="item.id" :label="item.label" :value="item.value">
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="StartDate" prop="StartDate">
            <el-date-picker type="date" v-model="form.startDate" :style="{ width: '100%' }" placeholder="请选择日期选择"
              clearable></el-date-picker>
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
        <el-table-column label="Title" prop="name" align="center" />
        <el-table-column label="Department" prop="organizationId" align="center" :formatter="locationFormatter" />
        <el-table-column label="Sponsor" prop="userEmail" align="center" />
        <el-table-column label="Re-Occurrence" prop="reOccurrence" align="center">
          <template slot-scope="scope">
            <el-switch v-model="scope.row.reOccurrence" active-color="red" inactive-color="Green"
              @change="changeEnabled(scope.row, scope.row.reOccurrence,)" />
          </template>
        </el-table-column>
        <el-table-column label="Process" prop="process" align="center" :formatter="processFormatter">
          <!-- <template slot-scope="scope">
            <span>{{ scope.row.process | processFilter }}</span>
          </template> -->
        </el-table-column>
        <el-table-column label="SOD" prop="source" align="center" :formatter="defectSourceFormatter" />
        <el-table-column label="StartDate" prop="startDate" align="center" />
        <el-table-column label="操作" align="center">
          <template slot-scope="{ row }">
            <el-button type="primary" size="mini" @click="handleUpdate(row)" icon="el-icon-edit" />
            <el-button type="danger" size="mini" @click="handleDelete(row)" icon="el-icon-delete" />
          </template>
        </el-table-column>
      </el-table>
      <pagination v-show="totalCount > 0" :total="totalCount" :page.sync="page" :limit.sync="listQuery.MaxResultCount"
        @pagination="getList" />
      <el-card>
        <el-tabs>
          <el-tab-pane label="A3Members" :lazy="false">
            <A3Member ref="A3MembersDetails"></A3Member>
          </el-tab-pane>
          <el-tab-pane label="Issues" :lazy="false">
            <el-row>
              <el-col :xs="14" :sm="15" :md="15" :lg="16" :xl="16">
                <Issue ref="IssueDetails"></Issue>
              </el-col>
              <el-col :xs="10" :sm="9" :md="9" :lg="8" :xl="8">
                <el-row style="margin-top: 80px;">
                  <!-- <el-card class="box-card" shadow="never"> -->
                  <el-upload ref="IssueUpload" :file-list="attachments.Issue"
                    :action="storageApi + '/api/storage/image/upload-img?name=' + form.name + Date.now()"
                    :on-success="(response, file, fileList) => { return handleSuccess(attachmentTypes.Issue, response, file, fileList) }"
                    list-type="picture-card">
                    <i class="el-icon-plus"></i>
                  </el-upload>
                  <!-- </el-card> -->
                </el-row>

                <el-row style="margin-top: 10px;">
                  <!-- <el-card class="box-card" shadow="never"> -->
                  <el-upload ref="IssueUploadDoc" :file-list="attachments.IssueDocs"
                    :action="storageApi + '/api/storage/doc/upload-doc?name=' + form.name + Date.now()"
                    :on-success="(response, file, fileList) => { return handleSuccess(attachmentTypes.IssueDocs, response, file, fileList) }"
                    list-type="text">
                    <el-button size="small" type="primary">上传文档</el-button>
                    <!-- <i class="el-icon-plus"></i> -->
                  </el-upload>
                  <!-- </el-card> -->
                </el-row>
              </el-col>
            </el-row>
          </el-tab-pane>
          <el-tab-pane label="ContainmentAction" :lazy="false">

            <el-col :xs="14" :sm="15" :md="15" :lg="16" :xl="16">
              <ContainmentAction ref="ContainmentActionDetails"></ContainmentAction>
            </el-col>
            <el-col :xs="10" :sm="9" :md="9" :lg="8" :xl="8">
              <el-row style="margin-top: 80px;">
                <el-upload ref="ContainmentActionUpload" :file-list="attachments.ContainmentAction"
                  :action="storageApi + '/api/storage/image/upload-img?name=' + form.name + Date.now()"
                  :on-success="(response, file, fileList) => { return handleSuccess(attachmentTypes.ContainmentAction, response, file, fileList) }"
                  list-type="picture-card">
                  <i class="el-icon-plus"></i>
                </el-upload>
              </el-row>
              <el-row style="margin-top: 10px;">
                <!-- <el-card class="box-card" shadow="never"> -->
                <el-upload ref="ContainmentActionUploadDoc" :file-list="attachments.ContainmentActionDocs"
                  :action="storageApi + '/api/storage/doc/upload-doc?name=' + form.name + Date.now()"
                  :on-success="(response, file, fileList) => { return handleSuccess(attachmentTypes.ContainmentActionDocs, response, file, fileList) }"
                  list-type="text">
                  <el-button size="small" type="primary">上传文档</el-button>
                  <!-- <i class="el-icon-plus"></i> -->
                </el-upload>
                <!-- </el-card> -->
              </el-row>
            </el-col>

          </el-tab-pane>
          <el-tab-pane label="RiskAssesment" :lazy="false">
            <el-col :xs="14" :sm="15" :md="15" :lg="16" :xl="16">

              <RiskAssesment ref="RiskAssesmentDetails"></RiskAssesment>
            </el-col>
            <el-col :xs="10" :sm="9" :md="9" :lg="8" :xl="8">
              <el-row style="margin-top: 80px;">
                <el-upload ref="RiskAssesmentUpload" :file-list="attachments.RiskAssesment"
                  :action="storageApi + '/api/storage/image/upload-img?name=' + form.name + Date.now()"
                  :on-success="(response, file, fileList) => { return handleSuccess(attachmentTypes.RiskAssesment, response, file, fileList) }"
                  list-type="picture-card">
                  <i class="el-icon-plus"></i>
                </el-upload>
              </el-row>
              <el-row style="margin-top: 10px;">
                <!-- <el-card class="box-card" shadow="never"> -->
                <el-upload ref="RiskAssesmentUploadDoc" :file-list="attachments.RiskAssesmentDocs"
                  :action="storageApi + '/api/storage/doc/upload-doc?name=' + form.name + Date.now()"
                  :on-success="(response, file, fileList) => { return handleSuccess(attachmentTypes.RiskAssesmentDocs, response, file, fileList) }"
                  list-type="text">
                  <el-button size="small" type="primary">上传文档</el-button>
                  <!-- <i class="el-icon-plus"></i> -->
                </el-upload>
                <!-- </el-card> -->
              </el-row>
            </el-col>
          </el-tab-pane>
          <el-tab-pane label="Cause" :lazy="false">
            <el-col :xs="14" :sm="15" :md="15" :lg="16" :xl="16">

              <Cause ref="CauseDetails"></Cause>
            </el-col>
            <el-col :xs="10" :sm="9" :md="9" :lg="8" :xl="8">
              <el-row style="margin-top: 80px;">
                <el-upload ref="CauseUpload" :file-list="attachments.Cause"
                  :action="storageApi + '/api/storage/image/upload-img?name=' + form.name + Date.now()"
                  :on-success="(response, file, fileList) => { return handleSuccess(attachmentTypes.Cause, response, file, fileList) }"
                  list-type="picture-card">
                  <i class="el-icon-plus"></i>
                </el-upload>
              </el-row>
              <el-row style="margin-top: 10px;">
                <!-- <el-card class="box-card" shadow="never"> -->
                <el-upload ref="RiskAssesmentUploadDoc" :file-list="attachments.CauseDocs"
                  :action="storageApi + '/api/storage/doc/upload-doc?name=' + form.name + Date.now()"
                  :on-success="(response, file, fileList) => { return handleSuccess(attachmentTypes.CauseDocs, response, file, fileList) }"
                  list-type="text">
                  <el-button size="small" type="primary">上传文档</el-button>
                  <!-- <i class="el-icon-plus"></i> -->
                </el-upload>
                <!-- </el-card> -->
              </el-row>
            </el-col>
          </el-tab-pane>
          <el-tab-pane label="CorrectiveAction" :lazy="false">
            <el-col :xs="14" :sm="15" :md="15" :lg="16" :xl="16">

            <CorrectiveAction ref="CorrectiveActionDetails"></CorrectiveAction>
            </el-col>
            <el-col :xs="10" :sm="9" :md="9" :lg="8" :xl="8">
              <el-row style="margin-top: 80px;">
                <el-upload ref="CorrectiveActionUpload" :file-list="attachments.CorrectiveAction"
                  :action="storageApi + '/api/storage/image/upload-img?name=' + form.name + Date.now()"
                  :on-success="(response, file, fileList) => { return handleSuccess(attachmentTypes.CorrectiveAction, response, file, fileList) }"
                  list-type="picture-card">
                  <i class="el-icon-plus"></i>
                </el-upload>
              </el-row>
              <el-row style="margin-top: 10px;">
                <!-- <el-card class="box-card" shadow="never"> -->
                <el-upload ref="CorrectiveActionUploadDoc" :file-list="attachments.CorrectiveActionDocs"
                  :action="storageApi + '/api/storage/doc/upload-doc?name=' + form.name + Date.now()"
                  :on-success="(response, file, fileList) => { return handleSuccess(attachmentTypes.CorrectiveActionDocs, response, file, fileList) }"
                  list-type="text">
                  <el-button size="small" type="primary">上传文档</el-button>
                  <!-- <i class="el-icon-plus"></i> -->
                </el-upload>
                <!-- </el-card> -->
              </el-row>
            </el-col>
          </el-tab-pane>
        </el-tabs>
      </el-card>
      <el-dialog :visible.sync="dialogVisible">
        <img width="100%" :src="dialogImageUrl" alt="">
      </el-dialog>
    </el-card>
  </div>
</template>
<script>

import Issue from '@/views/a3/issue'
import ContainmentAction from '@/views/a3/action/containment-action'
import RiskAssesment from '@/views/a3/risk-assesment'
import Cause from '@/views/a3/cause'
import CorrectiveAction from '@/views/a3/action/corrective-action'
import UserSelect from '@/views/components/user-select'
import A3Member from '@/views/a3/a3-member'

import Pagination from "@/components/Pagination";
import TreeSelect from "@riophae/vue-treeselect";
import '@riophae/vue-treeselect/dist/vue-treeselect.css'
import { LOAD_CHILDREN_OPTIONS } from "@riophae/vue-treeselect";

import permission from "@/directive/permission/index.js";

import baseService from "@/api/base";
import a3Service from "@/api/aaa";
import config from "../../../static/config";

const defaultForm = {
  id: null,
  a3Id: null,
  name: null,
  organizationId: null,
  userId: null,
  userEmail: null,
  reOccurrence: false,
  process: null,
  parentId: null,
  source: null,
  StartDate: null
};

const Types = {
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
};

export default {
  name: "A3",
  components: {
    Pagination,
    TreeSelect,
    Issue,
    ContainmentAction,
    RiskAssesment,
    Cause,
    CorrectiveAction,
    UserSelect,
    A3Member
  },
  directives: {
    permission
  },
  filters: {

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
        userEmail: [],
        process: [],
        parentId: [],
        source: [],
        StartDate: []
      },
      orgs: [],
      noTreeOrgs: [],
      form: Object.assign({}, defaultForm),
      list: null,
      processList: [],
      defectSources: [],
      totalCount: 0,
      listLoading: true,
      formLoading: false,
      listQuery: {
        a3Id: '',
        Filter: "",
        Sorting: "",
        SkipCount: 0,
        MaxResultCount: 10
      },
      page: 1,
      users: [],
      userLoadMoreConfig: {
        offset: 0,
        limit: 10,
        filter: '',
        page: 1,
        total: 0,
      },
      dialogFormVisible: false,
      multipleSelection: [],
      formTitle: "",
      isEdit: false,
      storageApi: config.storage.ip,
      selectA3Id: "",
      attachments: {
        ContainmentAction: [],
        RiskAssesment: [],
        Issue: [],
        Cause: [],
        CorrectiveAction: [],
        IssueDocs: [],
        ContainmentActionDocs: [],
        RiskAssesmentDocs: [],
        CauseDocs: [],
        CorrectiveActionDocs: [],
      },
      attachmentTypes: Types,
    };
  },
  computed: {},
  watch: {},
  created() {
    this.getProcessList();
    this.getDefectSource();
    this.getOrgNodes();
    this.getList();
  },
  beforeCreate() {
  },
  mounted() {

  },
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
      if (this.processList.length > 0) {
        return;
      }
      baseService.fetchOptionsList({ name: 'process' }).then(
        res => {
          this.processList = res.data.items;
        }
      )
    },
    getDefectSource() {
      if (this.defectSources.length > 0) {
        return;
      }
      baseService.fetchOptionsList({ name: 'defectSource' }).then(
        res => {
          this.defectSources = res.data.items;
        }
      )
    },
    // getUserList() {
    //   if (this.userLoadMoreConfig.page == 1 || this.userLoadMoreConfig.total - (this.userLoadMoreConfig.page - 1) * this.userLoadMoreConfig.limit > 0) {
    //     this.userLoadMoreConfig.offset = (this.userLoadMoreConfig.page - 1) * this.userLoadMoreConfig.limit;
    //     baseService.fetchUserList(this.userLoadMoreConfig).then(
    //       res => {
    //         let temp = res.data.items;
    //         this.userLoadMoreConfig.total = res.data.totalCount;

    //         this.users = this.userLoadMoreConfig.page == 1 ? temp : [...this.users, ...temp];
    //         this.userLoadMoreConfig.page++;
    //       }
    //     )
    //   }
    // },
    // userListRemoteMethod(inputValue) {
    //   if (inputValue && inputValue.length > 0) {
    //     this.userLoadMoreConfig.page = 1;
    //     this.users = [];
    //     this.userLoadMoreConfig.filter = inputValue;
    //     this.getUserList();
    //   } else {
    //     this.userLoadMoreConfig.filter = '';
    //     this.users = [];
    //   }
    // },
    // handleUserVisibleChange(val) {
    //   if (!val) {
    //     this.userLoadMoreConfig.filter = '';
    //     this.userLoadMoreConfig.page = 1;
    //     this.users = [];

    //   }
    //   else {
    //     this.getUserList();
    //   }

    // },
    processFormatter(row, column, val, index) {
      // this.getProcessList();
      let temp = this.processList.find(item => item.value === val);
      return temp ? temp.label : undefined;
      // return commonFormatter(this.processList, val);
    },
    defectSourceFormatter(row, column, val, index) {
      // return this.defectSources.find(item => item.value === val).label;
      // this.getDefectSource();
      let temp = this.defectSources.find(item => item.value === val);
      return temp ? temp.label : undefined;
      // return temp.label;
      // return commonFormatter(this.defectSources, val);
    },
    locationFormatter(row, column, val, index) {
      let temp = this.noTreeOrgs.find(item => item.id === val);
      return temp ? temp.label : undefined;
    },

    commonFormatter(list, val) {
      return list.find(item => item.value === val).label
    },
    handleProcessVisibleChange(val) {
      if (val) {
        this.getProcessList();
      }
    },
    handleDefectSourceVisibleChange(val) {
      if (val) {
        this.getDefectSource();
      }
    },
    getOrgNodes() {
      if (this.orgs.length > 0) {
        return;
      }
      baseService.fetchOrgNodesList().then((response) => {
        this.noTreeOrgs = response.data.items;
        this.loadTree(response.data);
      });
      console.log(this.orgs)
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
      this.form.a3Id = this.listQuery.a3Id;
      this.isEdit = false;
      this.dialogFormVisible = true;
      this.getOrgNodes();
      // this.getProcessList();
      // this.getDefectSource();
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

      // this.a3Id = row.id;
      //TODO: Maybe property is better, then set tabpanel to lazy mode
      this.selectA3Id = row.id;

      this.$refs.A3MembersDetails.listQuery.a3Id = row.id;
      this.$refs.A3MembersDetails.getList();

      this.getAttachment(this.attachmentTypes.Issue);
      this.getAttachment(this.attachmentTypes.IssueDocs);
      this.$refs.IssueDetails.listQuery.a3Id = row.id;
      this.$refs.IssueDetails.getList();

      this.getAttachment(this.attachmentTypes.ContainmentAction);
      this.getAttachment(this.attachmentTypes.ContainmentActionDocs);
      this.$refs.ContainmentActionDetails.listQuery.a3Id = row.id;
      this.$refs.ContainmentActionDetails.getList();

      this.getAttachment(this.attachmentTypes.RiskAssesment);
      this.getAttachment(this.attachmentTypes.RiskAssesmentDocs);
      this.$refs.RiskAssesmentDetails.listQuery.a3Id = row.id;
      this.$refs.RiskAssesmentDetails.getList();

      this.getAttachment(this.attachmentTypes.Cause);
      this.getAttachment(this.attachmentTypes.CauseDocs);
      this.$refs.CauseDetails.listQuery.a3Id = row.id;
      this.$refs.CauseDetails.getList();

      this.getAttachment(this.attachmentTypes.CorrectiveAction);
      this.getAttachment(this.attachmentTypes.CorrectiveActionDocs);
      this.$refs.CorrectiveActionDetails.listQuery.a3Id = row.id;
      this.$refs.CorrectiveActionDetails.getList();
    },
    cancel() {
      this.form = Object.assign({}, defaultForm);
      this.orgs = [];
      // this.processList = [];
      // this.defectSources = [];
      this.dialogFormVisible = false;
      this.$refs.form.clearValidate();
    },
    handleSuccessRiskAssesment() { },
    handleSuccess(attachmentType, response, file, fileList) {
      if (!this.selectA3Id)
        return;
      let item = {
        A3Id: this.selectA3Id,
        Type: attachmentType,
        Name: response.realName,
        Url: this.storageApi + response.url
      }
      this.$axios.posts('api/AAA/A3Attachment/data-post', item).then(response => {
        this.$notify({
          title: '成功',
          message: '新增成功',
          type: 'success',
          duration: 2000
        });
        this.getAttachment(attachmentType);
      }).catch(() => {
      });
    },

    getAttachment(type) {
      let query = {
        a3Id: this.selectA3Id,
        Type: type,

      }
      this.$axios.gets('api/AAA/A3Attachment', query).then(response => {
        this.attachments[type] = response.items;
      });
    },
    changeEnabled(data, val) {
      //   data.active = val ? "启用" : "停用";
      //   this.$confirm("是否" + data.active + data.name + "？", "提示", {
      //     confirmButtonText: "确定",
      //     cancelButtonText: "取消",
      //     type: "warning"
      //   })
      //     .then(() => {
      //       this.$axios
      //         .puts("/api/base/orgs/" + data.id, data)
      //         .then(response => {
      //           this.$notify({
      //             title: "成功",
      //             message: "更新成功",
      //             type: "success",
      //             duration: 2000
      //           });
      //         })
      //         .catch(() => {
      //           data.enabled = !data.enabled;
      //         });
      //     })
      //     .catch(() => {
      //       data.enabled = !data.enabled;
      //     });
      // 
    }

  }
};
</script>
<style></style>
