<template>
  <div class="app-container">
    <div class="head-container">
      <!-- 搜索 -->
      <el-input v-model="listQuery.Name" clearable size="small" placeholder="Program Name" style="width: 200px;"
        class="filter-item" @keyup.enter.native="handleFilter" />
      <el-input v-model="listQuery.Line" clearable size="small" placeholder="Line" style="width: 200px;"
        class="filter-item" @keyup.enter.native="handleFilter" /> 
      <el-date-picker size="small" v-model="listQuery.DateRange" type="daterange" range-separator="To"
        start-placeholder="Start" end-placeholder="End">
      </el-date-picker>
      <el-button class="filter-item" size="mini" type="success" icon="el-icon-search" @click="handleFilter">{{ $t('table.search') }}  
      </el-button>
      <!-- <el-button class="filter-item" size="mini" type="primary" icon="el-icon-plus" @click="handleCreate">新增
      </el-button>
      <el-button class="filter-item" size="mini" type="success" icon="el-icon-edit" @click="handleUpdate()">修改
      </el-button>
      <el-button slot="reference" class="filter-item" type="danger" icon="el-icon-delete" size="mini"
        @click="handleDelete()">删除</el-button> -->
    </div>
    <el-dialog :visible.sync="dialogFormVisible" :close-on-click-modal="false" @close="cancel()" :title="formTitle">
      <el-form ref="form" :model="form" :rules="rules" size="medium" label-width="100px">
        <el-form-item label="程序" prop="Name">
          <el-input v-model="form.name" placeholder="请输入程序" clearable :style="{ width: '100%' }"></el-input>
        </el-form-item>
        <!-- <el-form-item label="图片数量" prop="Windows">
          <el-input v-model="form.windows" placeholder="请输入FailureWindows" clearable
            :style="{ width: '100%' }"></el-input>
        </el-form-item> -->

        <el-form-item label="产线" prop="Line">
          <el-input v-model="form.line" placeholder="请输入产线" clearable :style="{ width: '100%' }"></el-input>
        </el-form-item>
        <el-form-item label="Failure" prop="FailureWindows">
          <el-input v-model="form.failure" placeholder="请输入FailureWindows" clearable
            :style="{ width: '100%' }"></el-input>
        </el-form-item>

        <el-form-item label="Good" prop="GoodWindows">
          <el-input v-model="form.good" placeholder="请输入GoodWindows" clearable :style="{ width: '100%' }"></el-input>
        </el-form-item>
        <el-form-item label="Slip" prop="SlipWindows">
          <el-input v-model="form.slip" placeholder="请输入SlipWindows" clearable :style="{ width: '100%' }"></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer">
        <el-button size="small" type="text" @click="cancel">取消</el-button>
        <el-button size="small" v-loading="formLoading" type="primary" @click="save">确认</el-button>
      </div>
    </el-dialog>
    <el-table ref="multipleTable" v-loading="listLoading" :data="list" size="small" style="width: 90%;"
      @sort-change="sortChange" @selection-change="handleSelectionChange" @expand-change="handleExpandChange"
      @row-click="handleRowClick">
      <el-table-column type="expand" width="44px">
        <template slot-scope="props">
          <div v-loading="props.row.loading">
            <el-table :data="props.row.children" size="mini">
              <el-table-column label="Id" prop="id" align="center" />
              <el-table-column label="Name" prop="name" align="center" />
              <el-table-column label="Failure" prop="failureWindows" align="center" />
              <el-table-column label="Good" prop="goodWindows" align="center" />
              <el-table-column label="Slip" prop="slipWindows" align="center" />
              <el-table-column label="HasError" prop="hasError" align="center">
                <template slot-scope="scope">
                  <el-switch v-model="scope.row.hasError" active-color="red" inactive-color="Green" disabled />
                </template>
              </el-table-column>
              <el-table-column label="IsChanged" prop="hasChanged" align="center">
                <template slot-scope="scope">
                  <el-switch v-model="scope.row.hasChanged" active-color="red" inactive-color="Green" disabled />
                </template>
              </el-table-column>
              <el-table-column label="ImageCount" align="center">
                <template slot-scope="scope">
                  {{ scope.row.goodWindows + scope.row.failureWindows + scope.row.slipWindows }}
                </template>
              </el-table-column>
              <el-table-column label="Cause" prop="cause" align="center">
                <template slot-scope="scope">
                  {{ getLabel(scope.row.cause) }}
                </template>
              </el-table-column>
            </el-table>
            <el-pagination background layout="prev, pager, next" :total="props.row.totalCount"
              :current-page="props.row.page" :page-size="props.row.MaxResultCount"
              @current-change="page => handleSubPageChange(props.row, page)"></el-pagination>
            <!-- <pagination v-show="props.row.totalCount > 0" :total="props.row.totalCount" :page.sync="props.row.page"
              :limit.sync="props.row.MaxResultCount" @pagination="getShapes(props.row)" /> -->
          </div>
        </template>
      </el-table-column>
      <el-table-column label="Id" prop="id" align="center" />
      <el-table-column label="Program" prop="name" align="center" />
      <el-table-column label="Line" prop="line" align="center" />
      <el-table-column label="ImageCount" prop="windows" align="center">
        <!-- <template slot-scope="scope">
          {{ scope.row.GoodWindows + scope.row.FailureWindows + scope.row.SlipWindows }}
        </template> -->
      </el-table-column>
      <el-table-column label="Date" prop="date" align="center" />
      <!-- <el-table-column label="操作" align="center">
        <template slot-scope="{row}">
          <el-button type="primary" size="mini" @click="handleUpdate(row)" icon="el-icon-edit" />
          <el-button type="danger" size="mini" @click="handleDelete(row)" icon="el-icon-delete" />
        </template>
      </el-table-column> -->
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
  windows: null,
  good: null,
  failure: null,
  slip: null,
  line: null,
  cause: null,
  creationTime: null
}
export default {
  name: 'ProgramCheckInfo',
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
        FailureWindows: [],
        Program: [],
        GoodWindows: [],
        SlipWindows: [],
        Line: [],
      },
      form: Object.assign({}, defaultForm),
      list: null,
      totalCount: 0,
      listLoading: true,
      rowLoading: false,
      formLoading: false,
      listQuery: {
        Filter: '',
        Name: '',
        Line: '',
        DateRange: [],
        StartDate: null,
        EndDate: null,
        Sorting: '',
        SkipCount: 0,
        MaxResultCount: 10
      },
      page: 1,
      dialogFormVisible: false,
      multipleSelection: [],
      formTitle: '',
      isEdit: false,
      CauseOptions: [],
      getLabel: value => value
    }
  },
  computed: {},
  watch: {},
  created() {
    this.getList()
  },
  mounted() { },
  methods: {
    getCauseOptions() {
      this.$axios.gets('api/base/dictDetails/list', { name: 'edbType' }).then(response => {
        this.CauseOptions = response.items;
        const { getLabel } = useDict(this.CauseOptions)
        this.getLabel = getLabel
      });
    },
    getList() {
      this.listLoading = true;
      if (this.listQuery.DateRange && this.listQuery.DateRange.length > 0) {
        [this.listQuery.StartDate, this.listQuery.EndDate] = this.listQuery.DateRange;
      } else {
        this.listQuery.StartDate = null;
        this.listQuery.EndDate = null;
      }
      this.listQuery.SkipCount = (this.page - 1) * this.listQuery.MaxResultCount;
      this.$axios.gets('api/FeedBack/ProgramInfo', this.listQuery).then(response => {
        this.list = response.items.map(item => ({
          //map for item expend, to make the loading is work
          ...item,
          loading: false,
          children: [],
          childrenLoaded: false,
          MaxResultCount: 10,
          totalCount: 0,
          page: 1
        })
        );
        // this.list = response.items;
        this.totalCount = response.totalCount;
        this.listLoading = false;
      });
    },
    fetchData(id) {
      this.$axios.gets('api/FeedBack/ProgramInfo/' + id).then(response => {
        this.form = response;
      });
    },
    handleFilter() {
      this.page = 1;
      this.getList();
    },
    handleCreate() {
      this.formTitle = '新增程序检测信息';
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
        this.$axios.posts('api/FeedBack/ProgramInfo/delete', params).then(response => {
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
      this.formTitle = '修改程序检测信息';
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
            this.$axios.posts('api/FeedBack/ProgramInfo/data-post', this.form).then(response => {
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
            this.$axios.posts('api/FeedBack/ProgramInfo/data-post', this.form).then(response => {
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
    handleExpandChange(row) {

      if (row.childrenLoaded) return;

      this.getShapes(row);

    },
    getShapes(row) {
      row.loading = true;
      this.getCauseOptions();


      // this.$set(row, 'loading', true);
      const start = (row.page - 1) * row.MaxResultCount;

      this.$axios.gets('api/FeedBack/ShapeInfo/', { ProgramId: row.id, SkipCount: start, MaxResultCount: row.MaxResultCount, Sorting: 'id asc' })
        .then(response => {
          row.children = response.items;
          row.totalCount = response.totalCount;
          this.$set(row, 'children', response.items);
          this.$set(row, 'childrenLoaded', true);
          this.$set(row, 'loading', false);
          // row.loading = false;
        });
    },
    handleSubPageChange(row, newPage) {
      row.page = newPage;
      this.getShapes(row);
    },
  }
}

</script>
<style></style>
