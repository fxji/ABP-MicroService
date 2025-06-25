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
        <el-form-item label="SerialNo" prop="SerialNo">
          <el-input v-model="form.SerialNo" placeholder="请输入SerialNo" clearable :style="{ width: '100%' }">
          </el-input>
        </el-form-item>
        <el-form-item label="MaterialNo" prop="MaterialNo">
          <el-input v-model="form.MaterialNo" placeholder="请输入MaterialNo" clearable :style="{ width: '100%' }">
          </el-input>
        </el-form-item>
        <el-form-item label="ComId" prop="ComId">
          <el-input v-model="form.ComId" placeholder="请输入ComId" clearable :style="{ width: '100%' }"></el-input>
        </el-form-item>
        <el-form-item label="Quantity" prop="Quantity">
          <el-input-number v-model="form.Quantity" placeholder="请输入Quantity"></el-input-number>
        </el-form-item>
        <el-form-item label="Project" prop="field110">
          <el-input v-model="form.field110" placeholder="Project" clearable :style="{ width: '100%' }">
          </el-input>
        </el-form-item>
        <el-form-item label="PieceNo" prop="PieceNo">
          <el-input-number v-model="form.PieceNo" placeholder="PieceNo"></el-input-number>
        </el-form-item>
        <el-form-item label="Position" prop="Position">
          <el-select v-model="form.Position" placeholder="请选择Position" clearable :style="{ width: '100%' }">
            <el-option v-for="(item, index) in PositionOptions" :key="index" :label="item.label" :value="item.value"
              :disabled="item.disabled"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="IsTop" prop="IsTop">
          <el-radio-group v-model="form.IsTop" size="undefined">
            <el-radio v-for="(item, index) in IsTopOptions" :key="index" :label="item.value"
              :disabled="item.disabled">{{ item.label }}</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="FailureMode" prop="FailureMode">
          <el-select v-model="form.FailureMode" placeholder="请选择FailureMode" clearable :style="{ width: '100%' }">
            <el-option v-for="(item, index) in FailureModeOptions" :key="index" :label="item.label" :value="item.value"
              :disabled="item.disabled"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="ComType" prop="ComType">
          <el-select v-model="form.ComType" placeholder="请输入ComType" clearable :style="{ width: '100%' }">
            <el-option v-for="(item, index) in ComTypeOptions" :key="index" :label="item.label" :value="item.value"
              :disabled="item.disabled"></el-option>
          </el-select>
        </el-form-item>
      </el-form>
      <div slot="footer">
        <el-button size="small" type="text" @click="cancel">取消</el-button>
        <el-button size="small" v-loading="formLoading" type="primary" @click="save">确认</el-button>
      </div>
    </el-dialog>
    <!--修改下方dialog样式，保持和其他dialog一致-->
    <el-dialog :visible.sync="dialogShareVisible" @close="handleShareCancel()" title="Share">
      <div class="form-page">
        <!-- 整体卡片容器 -->
        <el-row :gutter="10">
          <!-- <el-card shadow="hover"> -->
          <!-- 第一行：输入框 + 按钮 -->
          <div class="section mb-3">
            <el-row :gutter="16" align="middle">
              <el-col :span="6">
                <el-input v-model="keyword" placeholder="请输入关键词" clearable prefix-icon="el-icon-search" />
              </el-col>
              <el-col :span="3">
                <el-button type="primary" icon="el-icon-search" @click="onSearch">
                  查询
                </el-button>
              </el-col>
            </el-row>
          </div>

          <!-- 第二行：表格 -->
          <div class="section mb-3" v-if="tableData.length > 0">
            <el-table :data="tableData" border height="240" highlight-current-row style="width: 100%;"
              @row-click="onRowClick">
              <el-table-column prop="id" label="ID" width="80" />
              <el-table-column prop="name" label="姓名" />
              <el-table-column prop="email" label="邮箱" />
            </el-table>
          </div>
          <!-- </el-card> -->
        </el-row>
        <el-row :gutter="10">

          <el-card shadow="hover">

            <!-- 第三行：表单 -->
            <div class="section mb-3">
              <el-form :model="form" label-width="80px" label-position="right">
                <el-row :gutter="20">
                  <el-col :span="8">
                    <el-form-item label="姓名">
                      <el-input v-model="form.name" />
                    </el-form-item>
                  </el-col>
                  <el-col :span="8">
                    <el-form-item label="邮箱">
                      <el-input v-model="form.email" />
                    </el-form-item>
                  </el-col>
                </el-row>

                <el-form-item>
                  <el-button type="primary" @click="onSubmit">提交</el-button>
                  <el-button @click="onReset">重置</el-button>
                </el-form-item>
              </el-form>
            </div>
          </el-card>
        </el-row>
      </div>
    </el-dialog>
    <el-table ref="multipleTable" v-loading="listLoading" :data="list" size="small" style="width: 90%;"
      @sort-change="sortChange" @selection-change="handleSelectionChange" @row-click="handleRowClick">
      <el-table-column type="selection" width="44px"></el-table-column>
      <el-table-column label="SerialNo" prop="SerialNo" align="center" />
      <el-table-column label="MaterialNo" prop="MaterialNo" align="center" />
      <el-table-column label="ComId" prop="ComId" align="center" />
      <el-table-column label="Quantity" prop="Quantity" align="center" />
      <el-table-column label="Project" prop="field110" align="center" />
      <el-table-column label="PieceNo" prop="PieceNo" align="center" />
      <el-table-column label="Position" prop="Position" align="center" />
      <el-table-column label="IsTop" prop="IsTop" align="center" />
      <el-table-column label="FailureMode" prop="FailureMode" align="center" />
      <el-table-column label="ComType" prop="ComType" align="center" />
      <el-table-column label="操作" align="center">
        <template slot-scope="{row}">
          <el-button type="primary" size="mini" @click="handleAnalysis(row)" icon="el-icon-edit" />
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
const defaultForm = {
  id: null,
  SerialNo: '',
  MaterialNo: null,
  ComId: null,
  Quantity: 1,
  field110: null,
  PieceNo: 5,
  Position: null,
  IsTop: null,
  FailureMode: null,
  ComType: null,
}
export default {
  name: 'FailedInfo',
  components: {
    Pagination
  },
  directives: {
    permission
  },
  props: [],
  data() {
    return {
      rules: {
        SerialNo: [{
          required: true,
          message: '请输入SerialNo',
          trigger: 'blur'
        }],
        MaterialNo: [{
          required: true,
          message: '请输入MaterialNo',
          trigger: 'blur'
        }],
        ComId: [{
          required: true,
          message: '请输入ComId',
          trigger: 'blur'
        }],
        Quantity: [{
          required: true,
          message: '请输入Quantity',
          trigger: 'blur'
        }],
        field110: [{
          required: true,
          message: 'Project',
          trigger: 'blur'
        }],
        PieceNo: [{
          required: true,
          message: 'PieceNo',
          trigger: 'blur'
        }],
        Position: [{
          required: true,
          message: '请选择Position',
          trigger: 'change'
        }],
        IsTop: [{
          required: true,
          message: 'null',
          trigger: 'change'
        }],
        FailureMode: [{
          required: true,
          message: '请选择FailureMode',
          trigger: 'change'
        }],
        ComType: [{
          required: true,
          message: '请输入ComType',
          trigger: 'change'
        }],
      },
      form: Object.assign({}, defaultForm),
      list: null,
      totalCount: 0,
      listLoading: true,
      formLoading: false,
      listQuery: {
        Filter: '',
        Sorting: '',
        SkipCount: 0,
        MaxResultCount: 10
      },
      page: 1,
      dialogFormVisible: false,
      dialogShareVisible: false,
      multipleSelection: [],
      formTitle: '',
      isEdit: false,
      PositionOptions: [{
        "label": "选项一",
        "value": "Position1"
      }, {
        "label": "选项二",
        "value": "Position2"
      }],
      IsTopOptions: [{
        "label": "Bottom",
        "value": "false"
      }, {
        "label": "Top",
        "value": "true"
      }],
      FailureModeOptions: [{
        "label": "选项二",
        "value": "FailureMode2"
      }, {
        "label": "选项一",
        "value": "FailureMode1"
      }],
      ComTypeOptions: [{
        "label": "选项二",
        "value": "ComType2"
      }, {
        "label": "选项一",
        "value": "ComType1"
      }],
      keyword: '',
      tableData: [],
      form: {
        name: '',
        email: '',
      },
    }
  },
  computed: {},
  watch: {},
  created() {
    this.getList()
  },
  mounted() { },
  methods: {
    getList() {
      this.listLoading = true;
      this.listQuery.SkipCount = (this.page - 1) * this.listQuery.MaxResultCount;
      this.$axios.gets('api/feedback/failedinfo', this.listQuery).then(response => {
        this.list = response.items;
        this.totalCount = response.totalCount;
        this.listLoading = false;
      });
    },
    fetchData(id) {
      this.$axios.gets('api/feedback/failedinfo/' + id).then(response => {
        this.form = response;
      });
    },
    handleFilter() {
      this.page = 1;
      this.getList();
    },
    handleCreate() {
      this.formTitle = '新增表单';
      this.isEdit = false;
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
        this.$axios.posts('api/feedback/failedinfo/delete', params).then(response => {
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
    handleAnalysis(row) {
      this.dialogShareVisible = true;
    },
    handleAnalysisClose() {
      this.dialogShareVisible = false;
    },
    save() {
      this.$refs.form.validate(valid => {
        if (valid) {
          this.formLoading = true;
          this.form.roleNames = this.checkedRole;
          if (this.isEdit) {
            this.$axios.posts('api/feedback/failedinfo/data-post', this.form).then(response => {
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
            this.$axios.posts('api/feedback/failedinfo/data-post', this.form).then(response => {
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
    onSearch() {
      // 模拟搜索
      this.tableData = [
        { id: 1, name: '张三', email: 'zhangsan@example.com' },
        { id: 2, name: '李四', email: 'lisi@example.com' },
      ];
    },
    onRowClick(row) {
      this.form = { ...row };
    },
    onSubmit() {
      console.log('提交表单：', this.form);
    },
    onReset() {
      this.form = { name: '', email: '' };
    },

  }
}

</script>
<style scoped>
.form-page {
  padding: 30px;
  background-color: #f5f7fa;
  min-height: 100vh;
}

.section {
  margin-bottom: 20px;
}

.mb-3 {
  margin-bottom: 24px;
}
</style>
