<template>
    <div class="app-container dashboard-editor-container">
        <!---->
        <el-row>
            <el-card class="card">
                <div slot="header" align="center" class="header">
                    <span>General</span>
                </div>
                <div>


                    <el-table label="a3" ref="a3" :data="generalData" size="small" style="width: 90%;">
                        <el-table-column label="ID" prop="id" align="center">
                            <template slot-scope="{row}">
                                <span class="link-type" @click="handleJumpTo(row)">{{ row.id }}</span>
                            </template>
                        </el-table-column>
                        <el-table-column label="Title" prop="name" align="center" />
                        <el-table-column label="Department" prop="organizationId" align="center"
                            :formatter="locationFormatter" />
                        <el-table-column label="Sponsor" prop="userEmail" align="center" />
                        <el-table-column label="Re-Occurrence" prop="reOccurrence" align="center">
                            <template slot-scope="scope">
                                <el-switch disabled v-model="scope.row.reOccurrence" active-color="red"
                                    inactive-color="Green" />
                            </template>
                        </el-table-column>
                        <el-table-column label="Process" prop="process" align="center" :formatter="processFormatter">
                        </el-table-column>
                        <el-table-column label="SOD" prop="source" align="center" :formatter="defectSourceFormatter" />
                        <el-table-column label="StartDate" prop="startDate" align="center" />

                    </el-table>
                </div>
            </el-card>
        </el-row>
        <el-row :gutter="16">
            <el-col :xs="24" :sm="24" :lg="16">
                <el-card class="card">
                    <div slot="header">
                        Issue
                    </div>
                    <div>
                        <el-table ref="issue" :data="list.Issue" size="small" style="width: 90%;">
                            <el-table-column label="Title" prop="name" />
                            <el-table-column label="Problem Type" prop="type" :formatter="issueTypeFormatter" />
                            <el-table-column label="Customer Group" prop="customerGroup" />
                            <el-table-column label="Project Name" prop="project" />
                            <el-table-column label="OccurrenceDate" prop="occurrenceDate" />
                            <el-table-column label="Failure Qty/Rate" prop="rate" />

                        </el-table>
                    </div>
                </el-card>
            </el-col>
            <el-col :xs="24" :sm="24" :lg="8">
                <el-card class="card">
                    <div slot="header">
                        AttachFiles
                    </div>
                    <attachment :Id="form.a3Id" :category="category.Issue"></attachment>

                </el-card>
            </el-col>


        </el-row>

        <el-row :gutter="16">
            <el-col :xs="24" :sm="24" :lg="16">
                <el-card class="card">
                    <div slot="header">
                        ContainmentAction
                    </div>
                    <el-table ref="containmentAction" :data="list.ContainmentAction" size="small" style="width: 90%;">
                        <el-table-column label="Activities" prop="name" />
                        <el-table-column label="Responsibility" prop="responsibility" />
                        <el-table-column label="Type" prop="type" :formatter="actionTypeFormate" />
                        <el-table-column label="Status" prop="status" :formatter="statusFormate" />

                    </el-table>
                </el-card>
            </el-col>
            <el-col :xs="24" :sm="24" :lg="8">
                <el-card class="card">
                    <div slot="header">
                        AttachFiles
                    </div>
                    <attachment :Id="form.a3Id" :category="category.ContainmentAction"></attachment>

                </el-card>
            </el-col>

        </el-row>

        <el-row :gutter="16">
            <el-col :xs="24" :sm="24" :lg="16">
                <el-card class="card">
                    <div slot="header">RiskAssessment</div>
                    <el-table ref="risk" :data="list.RiskAssessment" size="small" style="width: 90%;">
                        <el-table-column label="Factor" prop="name" />
                        <el-table-column label="SafetyRelevant" prop="safetyRelevant">
                            <template slot-scope="scope">
                                <el-switch disabled v-model="scope.row.safetyRelevant" active-color="Green" />
                            </template>
                        </el-table-column>
                        <el-table-column label="Functionally" :formatter="levelFormate" prop="functionally" />
                        <el-table-column label="Probability" :formatter="levelFormate" prop="probability" />

                    </el-table>
                </el-card>
            </el-col>
            <el-col :xs="24" :sm="24" :lg="8">
                <el-card class="card">
                    <div slot="header">AttachFiles</div>
                    <attachment :Id="form.a3Id" :category="category.RiskAssessment"></attachment>

                </el-card>
            </el-col>

        </el-row>

        <el-row :gutter="16">
            <el-col :xs="24" :sm="24" :lg="16">
                <el-card class="card">
                    <div slot="header">Cause</div>
                    <el-table ref="cause" :data="list.Cause" size="small" style="width: 90%;">
                        <el-table-column label="Type" prop="type" :formatter="typeFormate" />
                        <el-table-column label="Title" prop="name" />
                        <el-table-column label="Status" prop="status" :formatter="statusFormate" />

                    </el-table>

                </el-card>
            </el-col>
            <el-col :xs="24" :sm="24" :lg="8">
                <el-card class="card">
                    <div slot="header">AttachFiles</div>
                    <attachment :Id="form.a3Id" :category="category.Cause"></attachment>

                </el-card>
            </el-col>

        </el-row>

        <el-row :gutter="16">
            <el-col :xs="24" :sm="24" :lg="16">
                <el-card class="card">
                    <div slot="header">CorrectiveAction</div>
                    <el-table ref="CorrectiveAction" :data="list.CorrectiveAction" size="small" style="width: 90%;">
                        <el-table-column label="Action" prop="name" />
                        <el-table-column label="Responsibility" prop="responsibility" />
                        <el-table-column label="Status" prop="status" :formatter="statusFormate" />
                        <el-table-column label="DueDate" prop="dueDate" />

                    </el-table>
                </el-card>
            </el-col>
            <el-col :xs="24" :sm="24" :lg="8">
                <el-card class="card">
                    <div slot="header">AttachFiles</div>
                    <attachment :Id="form.a3Id" :category="category.CorrectiveAction"></attachment>

                </el-card>
            </el-col>

        </el-row>
    </div>

</template>

<script>
import Attachment from './../../components/attachment/index.vue'
import baseService from "@/api/base";
const defaultForm = {
    id: '',
    // a3Id: '15d7702c-385a-b8de-ecf3-3a0de7b020fa',
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
export default {
    name: 'A3Details',
    components: { Attachment },
    data() {
        return {
            form: Object.assign({}, defaultForm),
            generalData: [],
            list: {
                // Issue: [],
                // ContainmentAction: [],
                // RiskAssessment: [],
                // Cause: [],
                // CorrectiveAction: []
            },
            category: {
                Issue: 'Issue',
                ContainmentAction: 'ContainmentAction',
                Cause: 'Cause',
                RiskAssessment: 'RiskAssessment',
                CorrectiveAction: 'CorrectiveAction'
            },
            dicts: {
                // issueType: [],
                // process: [],
                // defectSource: [],
                // actionType: [],
                // status: [],
                // levels: [],
                // causeTypes: [],
                // causeStatus: [],
            },
            dictType: {
                issueType: 'issueType',
                process: 'process',
                defectSource: 'defectSource',
                actionType: 'actionType',
                status: 'status',
                levels: 'levels',
                causeTypes: 'causeTypes',
                causeStatus: 'causeStatus',
            },
            noTreeOrgs: []
        }
    },
    created() {
        this.setA3Id();
        this.fetchDicts();
        this.getOrgNodes();
        setTimeout(() => {
            this.fetchData();
        }, 500);
    },
    methods: {
        handleJumpTo(row) {
            let location = {
                path: '/A3',
                query: { a3Id: row.id }
            }
            this.$router.push(location)
        },
        setA3Id() {
            this.form.a3Id = this.$route.params.id;
            // console.log('接收到参数id:' + id)
            // this.form.a3Id = id ? id : this.defaultForm.a3Id;
        },
        fetchDicts() {
            Object.keys(this.dictType).forEach(
                key => {
                    this.getDicts(this.dictType[key])
                }
            );
        },
        fetchData() {
            this.getGeneral();
            Object.keys(this.category).forEach
                (key => {
                    this.getList(this.category[key]);
                    // this.getImages(item);
                    // this.getDocs(item);
                });

        },
        getGeneral() {
            this.$axios.gets('api/AAA/A3/' + this.form.a3Id).then(response => {
                this.generalData = [response];
                // console.log(response.data);
                // console.log(this.generalData);
            });

        },

        getList(category) {
            this.$axios.gets('api/AAA/' + category, { a3Id: this.form.a3Id }).then(response => {
                //强制页面刷新数据
                // this.list[category] = response.items;
                this.$set(this.list, category, response.items);
            });
        },
        getDicts(type) {
            baseService.fetchOptionsList({ name: type }).then(
                res => {
                    this.$set(this.dicts, type, res.data.items);
                }
            )
        },
        getOrgNodes() {
            baseService.fetchOrgNodesList().then((response) => {
                this.noTreeOrgs = response.data.items;
                // this.loadTree(response.data);
            });
        },
        issueTypeFormatter(row, column, val, index) {
            // this.getIssueTypeList();
            let temp = this.dicts[this.dictType.issueType].find(item => item.value === val);
            // console.log(temp);
            return temp ? temp.label : 'undefined';
            // return this.commonFormatter(this.issueTypes, val);
        },

        actionTypeFormate(row, column, val, index) {
            let temp = this.dicts.actionType.find(item => item.value === val);
            // console.log(temp);
            return temp ? temp.label : 'undefined';
        },

        typeFormate(row, column, val, index) {
            let temp = this.dicts.causeTypes.find(item => item.value === val);
            // console.log(temp);
            return temp ? temp.label : 'undefined';
        },

        statusFormate(row, column, val, index) {
            let temp = this.dicts.status.find(item => item.value === val);
            // console.log(temp);
            return temp ? temp.label : 'undefined';
        },

        levelFormate(row, column, val, index) {
            let temp = this.dicts.levels.find(item => item.value === val);
            // console.log(temp);
            return temp ? temp.label : 'undefined';
        },
        processFormatter(row, column, val, index) {
            // this.getProcessList();
            let temp = this.dicts.process.find(item => item.value === val);
            return temp ? temp.label : undefined;
            // return commonFormatter(this.processList, val);
        },
        defectSourceFormatter(row, column, val, index) {
            // return this.defectSources.find(item => item.value === val).label;
            // this.getDefectSource();
            let temp = this.dicts.defectSource.find(item => item.value === val);
            return temp ? temp.label : undefined;
            // return temp.label;
            // return commonFormatter(this.defectSources, val);
        },
        locationFormatter(row, column, val, index) {
            let temp = this.noTreeOrgs.find(item => item.id === val);
            return temp ? temp.label : undefined;
        },

    }
}
</script>

<style lang="scss" scoped>
.dashboard-editor-container {
    padding: 16px;
    background-color: rgb(240, 242, 245);
    position: relative;


    .card {
        background: rgb(255, 255, 255);
        padding: 16px 16px 0;
        margin-bottom: 16px;
    }
}
</style>
