<template>
  <div class="app-container">
    <div class="head-container">
      <!-- 搜索 -->
      <el-input v-model="listQuery.Filter" clearable size="small" placeholder="搜索..." style="width: 200px;"
        class="filter-item" @keyup.enter.native="handleFilter" />
      <el-button class="filter-item" size="mini" type="success" icon="el-icon-search" @click="handleFilter">搜索
      </el-button>
      <!-- <el-button class="filter-item" size="mini" type="primary" icon="el-icon-plus" @click="handleCreate">新增
        </el-button>
        <el-button class="filter-item" size="mini" type="success" icon="el-icon-edit" @click="handleUpdate()">修改
        </el-button>
        <el-button slot="reference" class="filter-item" type="danger" icon="el-icon-delete" size="mini"
          @click="handleDelete()">删除</el-button> -->
    </div>

    <el-table ref="multipleTable" v-loading="listLoading" :data="list" size="small" style="width: 90%;"
      @sort-change="sortChange" @selection-change="handleSelectionChange" @row-click="handleRowClick">
      <el-table-column type="selection" width="44px"></el-table-column>
      <el-table-column label="CreatorId" prop="creatorId" align="center" />
      <el-table-column label="Comments" prop="comments" align="center" />
      <el-table-column label="Date" prop="creationTime" align="center" />
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
import Pagination from "@/components/Pagination";
import permission from "@/directive/permission/index.js";
const defaultForm = {
  id: null,
  UserId: null,
  A3Id: null,
  Comments: null,
  Date: null,
}
export default {
  name: 'ConfirmInfo',
  components: {
    Pagination
  },
  directives: {
    permission
  },
  props: [
    'a3Id'
  ],
  
  data() {
    return {
      list: null,
      totalCount: 0,
      listLoading: true,
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
    }
  },
  computed: {},
  watch: {
    a3Id: {
      handler: function (newVal, oldVal) {
        this.listQuery.a3Id = newVal;
        this.getList();
      },
      immediate: true
    }
  },

  mounted() { },
  methods: {
    getList() {
      this.listLoading = true;
      this.listQuery.SkipCount = (this.page - 1) * this.listQuery.MaxResultCount;
      this.$axios.gets('api/AAA/confirmInfo', this.listQuery).then(response => {
        this.list = response.items;
        this.totalCount = response.totalCount;
        this.listLoading = false;
      });
    },
    
    handleFilter() {
      this.page = 1;
      this.getList();
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

  }
}

</script>
<style></style>