<template>
  <div class="app-container">
    <div class="head-container">
      <!-- 搜索 -->
      <el-input v-model="listQuery.Name" clearable size="small" placeholder="封装名" style="width: 200px;"
        class="filter-item" @keyup.enter.native="handleFilter" />
      <el-input v-model="listQuery.Line" clearable size="small" placeholder="产线" style="width: 200px;"
        class="filter-item" @keyup.enter.native="handleFilter" />

      <div style="border: 1px solid #dcdfe6; padding: 5px; border-radius: 4px;">
        <el-switch v-model="listQuery.HasError" active-color="#ff4949" inactive-color="#13ce66" active-text="Error" />
      </div>
      <div style="border: 1px solid #dcdfe6; padding: 5px; border-radius: 4px;">
        <el-switch v-model="listQuery.HasChanged" active-color="#ff4949" inactive-color="#13ce66"
          active-text="Changed" />
      </div>


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
        <el-form-item label="Name" prop="name">
          <el-input v-model="form.name" placeholder="请输入Name" clearable :style="{ width: '100%' }"></el-input>
        </el-form-item>
        <el-form-item label="程序" prop="programId">
          <el-input v-model="form.programId" placeholder="请输入程序" clearable :style="{ width: '100%' }">
          </el-input>
        </el-form-item>
        <el-form-item label="产线" prop="line">
          <el-input v-model="form.line" placeholder="请输入产线" clearable :style="{ width: '100%' }"></el-input>
        </el-form-item>
        <el-form-item label="FailureWindows" prop="failureWindows">
          <el-input v-model="form.failureWindows" placeholder="请输入FailureWindows" clearable
            :style="{ width: '100%' }"></el-input>
        </el-form-item>
        <el-form-item label="GoodWindows" prop="goodWindows">
          <el-input v-model="form.goodWindows" placeholder="请输入GoodWindows" clearable
            :style="{ width: '100%' }"></el-input>
        </el-form-item>
        <el-form-item label="SlipWindows" prop="slipWindows">
          <el-input v-model="form.slipWindows" placeholder="请输入SlipWindows" clearable
            :style="{ width: '100%' }"></el-input>
        </el-form-item>
        <el-form-item label="IsChanged" prop="hasChanged">
          <el-switch v-model="form.hasChanged" :active-value="undefined" :inactive-value="undefined">
          </el-switch>
        </el-form-item>
        <el-form-item label="Cause" prop="cause">
          <el-select v-model="form.cause" placeholder="请选择原因" clearable :style="{ width: '100%' }">
            <el-option v-for="(item, index) in CauseOptions" :key="index" :label="item.label" :value="item.value"
              :disabled="item.disabled"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="产生时间" prop="date">
          <el-input v-model="form.creationTime" placeholder="请输入产生时间" clearable :style="{ width: '100%' }">
          </el-input>
        </el-form-item>

      </el-form>
      <div slot="footer">
        <el-button size="small" type="text" @click="cancel">取消</el-button>
        <el-button size="small" v-loading="formLoading" type="primary" @click="save">确认</el-button>
      </div>
    </el-dialog>
    <el-dialog :visible.sync="dialogFormVisible2" :close-on-click-modal="false" @close="cancel()" :title="formTitle">
      <el-form ref="form4MultipleUpdate" :model="form" :rules="rules" size="medium" label-width="100px">


        <el-form-item label="Cause" prop="cause">
          <el-select v-model="cause4multipleUpdate" placeholder="请选择原因" clearable :style="{ width: '100%' }">
            <el-option v-for="(item, index) in CauseOptions" :key="index" :label="item.label" :value="item.value"
              :disabled="item.disabled"></el-option>
          </el-select>
        </el-form-item>

      </el-form>
      <div slot="footer">
        <el-button size="small" type="text" @click="cancel">取消</el-button>
        <el-button size="small" v-loading="formLoading" type="primary" @click="saveMultiple">确认</el-button>
      </div>
    </el-dialog>
    <el-table ref="multipleTable" v-loading="listLoading" :data="list" size="small" style="min-width: 720px;"
      @sort-change="sortChange" @selection-change="handleSelectionChange" @row-click="handleRowClick">
      <el-table-column type="selection" width="44px"></el-table-column>
      <!-- <el-table-column label="Id" prop="id" align="center" /> -->
      <el-table-column label="Name" prop="name" align="center" width="220" fixed />
      <el-table-column label="程序" prop="programName" align="center" width="160" />
      <el-table-column label="产线" prop="line" align="center" width="120" />
      <el-table-column label="Failure" prop="failureWindows" align="center" width="120" />
      <el-table-column label="Good" prop="goodWindows" align="center" width="120" />
      <el-table-column label="Slip" prop="slipWindows" align="center" width="120" />
      <el-table-column label="Error" prop="hasError" align="center" width="120">
        <template slot-scope="scope">
          <el-switch v-model="scope.row.hasError" active-color="red" inactive-color="Green" disabled />
        </template>
      </el-table-column>
      <el-table-column label="Changed" prop="hasChanged" align="center" width="120">
        <template slot-scope="scope">
          <el-switch v-model="scope.row.hasChanged" active-color="red" inactive-color="Green" disabled />
        </template>
      </el-table-column>
      <el-table-column label="Count" align="center" width="120">
        <template slot-scope="scope">
          {{ scope.row.goodWindows + scope.row.failureWindows + scope.row.slipWindows }}
        </template>
      </el-table-column>
      <el-table-column label="Cause" prop="cause" align="center" width="220">
        <template slot-scope="scope">
          {{ getLabel(scope.row.cause) }}
        </template>
      </el-table-column>
      <el-table-column label="Date" prop="date" align="center" width="220" />

      <el-table-column label="操作" align="center" width="220" fixed="right">
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
import { useDict } from "@/utils/dict-formatter";
const defaultForm = {
  id: null,
  name: null,
  programId: null,
  programName: null,
  line: null,
  failureWindows: null,
  goodWindows: null,
  slipWindows: null,
  hasChanged: null,
  cause: undefined,
  date: '',
}
export default {
  name: 'ShapeCheckInfo',
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
        Name: [],
        ProgramId: [],
        Line: [],
        FailureWindows: [],
        GoodWindows: [],
        SlipWindows: [],
        HasChanged: [],
        Cause: [],
        CreateTime: [],
      },
      form: Object.assign({}, defaultForm),
      cause4multipleUpdate: null,
      list: null,
      totalCount: 0,
      listLoading: true,
      formLoading: false,
      listQuery: {
        Filter: '',
        Name: '',
        Line: '',
        HasError: false,
        HasChanged: false,
        Sorting: '',
        SkipCount: 0,
        MaxResultCount: 10
      },
      page: 1,
      dialogFormVisible: false,
      dialogFormVisible2: false,
      multipleSelection: [],
      formTitle: '',
      isEdit: false,
      CauseOptions: [],
      getLabel: value => value // ✅ 提前定义为 null，防止模板访问不到
    }
  },
  computed: {},
  watch: {},
  created() {
    this.getList();
    this.getCauseOptions();

  },
  mounted() { },
  methods: {
    getCauseOptions() {
      this.$axios.gets('api/base/dictDetails/list', { name: 'causeTypes' }).then(response => {
        this.CauseOptions = response.items;
        const { getLabel } = useDict(this.CauseOptions)
        this.getLabel = getLabel
      });
    },
    getList() {
      this.listLoading = true;
      this.listQuery.SkipCount = (this.page - 1) * this.listQuery.MaxResultCount;
      this.$axios.gets('api/FeedBack/ShapeInfo', this.listQuery).then(response => {
        this.list = response.items;
        this.totalCount = response.totalCount;
        this.listLoading = false;
      });
    },
    fetchData(id) {
      this.$axios.gets('api/FeedBack/ShapeInfo/' + id).then(response => {
        this.form = response;
      });
    },
    handleFilter() {
      this.page = 1;
      this.getList();
    },
    handleCreate() {
      this.formTitle = '新增封装检测信息';
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
        this.$axios.posts('api/FeedBack/ShapeInfo/delete', params).then(response => {
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
      this.formTitle = '修改封装检测信息';
      this.isEdit = true;
      if (row) {
        this.fetchData(row.id);
        this.dialogFormVisible = true;
      }
      else {
        if (this.multipleSelection.length === 0) {
          this.$message({
            message: '未选择',
            type: 'warning'
          });
          return;
        }
        this.cause4multipleUpdate = null;
        this.dialogFormVisible2 = true;
      }

    },

    save() {
      this.$refs.form.validate(valid => {
        if (valid) {
          this.formLoading = true;
          this.form.roleNames = this.checkedRole;
          if (this.isEdit) {
            this.$axios.posts('api/FeedBack/ShapeInfo/data-post', this.form).then(response => {
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
            this.$axios.posts('api/FeedBack/ShapeInfo/data-post', this.form).then(response => {
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
    saveMultiple() {
      var params = [];

      this.multipleSelection.forEach(element => {
        let id = element.id;
        params.push(id);
      });
      alert = '选中项';
      this.$refs.form4MultipleUpdate.validate(valid => {
        if (valid) {
          this.formLoading = true;
          // this.form.roleNames = this.checkedRole;
          this.$axios.patchs('api/FeedBack/ShapeInfo/batch-update-cause', { ids: params, cause: this.cause4multipleUpdate }).then(response => {
            this.formLoading = false;
            this.$notify({
              title: '成功',
              message: '新增成功',
              type: 'success',
              duration: 2000
            });
            this.dialogFormVisible2 = false;
            this.getList();
          }).catch(() => {
            this.formLoading = false;
          });
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
      this.dialogFormVisible2 = false;
      this.$refs.form.clearValidate();
      cause4multipleUpdate = null;
    },
  }
}

</script>
<style></style>
