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
                        <el-table-column label="ID" prop="id" align="center" />
                        <el-table-column label="Title" prop="name" align="center" />
                        <!-- <el-table-column label="Department" prop="organizationId" align="center"
                        :formatter="locationFormatter" /> -->
                        <el-table-column label="Sponsor" prop="userEmail" align="center" />
                        <el-table-column label="Re-Occurrence" prop="reOccurrence" align="center">
                            <template slot-scope="scope">
                                <el-switch v-model="scope.row.reOccurrence" active-color="red" inactive-color="Green" />
                            </template>
                        </el-table-column>
                        <!-- <el-table-column label="Process" prop="process" align="center" :formatter="processFormatter">

                    </el-table-column> -->
                        <!-- <el-table-column label="SOD" prop="source" align="center" :formatter="defectSourceFormatter" /> -->
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
                        <el-table ref="issue" :data="list.issue" size="small" style="width: 90%;">
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
                <el-card>
                    <div slot="header">
                        Attachment
                    </div>
                    <div>
                        <el-row>
                            <div v-for="(item, index) in attachFiles.issue" :key="item.id">
                                <el-image :key="index" style="width: 100px; height: 100px" :src="item.url"
                                    fit="fill"></el-image>
                            </div>
                        </el-row>

                        <el-row>
                            <div v-for="(item, index) in attachDocs.issue" :key="item.id" style="margin-top: 16px;">
                                <el-link :key="index" icon="el-icon-document" :href="item.url">{{ item.name
                                    }}</el-link>
                            </div>
                        </el-row>
                    </div>
                </el-card>
            </el-col>


        </el-row>

        <el-row :gutter="16">
            <el-col :xs="24" :sm="24" :lg="16">
                <el-card class="card">
                    <div slot="header" >
                        ContainmentAction
                    </div>
                    <el-table ref="action1" :data="list.containmentAction" size="small" style="width: 90%;">
                        <el-table-column label="Activities" prop="name" />
                        <el-table-column label="Responsibility" prop="responsibility" />
                        <el-table-column label="Type" prop="type" :formatter="actionTypeFormate" />
                        <el-table-column label="Status" prop="status" :formatter="statusFormate" />

                    </el-table>
                </el-card>
            </el-col>
            <el-col :xs="24" :sm="24" :lg="8">
                <el-card class="card" >
                    <div slot="header" >
                        attachFiles
                    </div>
                    <el-row>
                        <el-image v-for="item in attachFiles.containmentAction" :key="item.id"
                            style="width: 100px; height: 100px" :src="item.url" fit="fill"></el-image>
                    </el-row>
                    <el-row>
                        <div v-for="(item, index) in attachDocs.containmentAction" :key="item.id">
                            <el-link :key="index" icon="el-icon-document" :href="item.url">{{ item.name }}</el-link>
                        </div>
                    </el-row>
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
                                <el-switch :disabled=true v-model="scope.row.safetyRelevant" active-color="Green" />
                            </template>
                        </el-table-column>
                        <el-table-column label="Functionally" :formatter="levelFormate" prop="functionally" />
                        <el-table-column label="Probability" :formatter="levelFormate" prop="probability" />

                    </el-table>
                </el-card>
            </el-col>
            <el-col :xs="24" :sm="24" :lg="8">
                <el-card class="card">
                    <div slot="header">attachFiles</div>
                    
                    <el-row>
                        <div v-for="(item, index) in attachFiles.RiskAssessment" :key="item.id">
                            <el-image :key="index" style="width: 100px; height: 100px" :src="item.url"
                                fit="fill"></el-image>
                        </div>
                    </el-row>
                    <el-row>
                        <div v-for="(item, index) in attachDocs.RiskAssessment" :key="item.id">
                            <el-link :key="index" icon="el-icon-document" :href="item.url">{{ item.name }}</el-link>
                        </div>
                    </el-row>
                </el-card>
            </el-col>

        </el-row>

        <el-row :gutter="16">
            <el-col :xs="24" :sm="24" :lg="16">
                <el-card class="card">
                    <div slot="header">Cause</div>
                    <el-table ref="cause" :data="list.cause" size="small" style="width: 90%;">
                        <el-table-column label="Type" prop="type" :formatter="typeFormate" />
                        <el-table-column label="Title" prop="name" />
                        <el-table-column label="Status" prop="status" :formatter="statusFormate" />

                    </el-table>

                </el-card>
            </el-col>
            <el-col :xs="24" :sm="24" :lg="8">
                <el-card class="card">
                    <div slot="header">attachFiles</div>
                    <el-row>
                        <div v-for="(item, index) in attachFiles.cause" :key="item.id">
                            <el-image :key="index" style="width: 100px; height: 100px" :src="item.url"
                                fit="fill"></el-image>
                        </div>
                    </el-row>
                    <el-row>
                        <div v-for="(item, index) in attachDocs.cause" :key="item.id">
                            <el-link :key="index" icon="el-icon-document" :href="item.url">{{ item.name }}</el-link>
                        </div>
                    </el-row>
                </el-card>
            </el-col>

        </el-row>

        <el-row :gutter="16">
            <el-col :xs="24" :sm="24" :lg="16">
                <el-card class="card">
                    <div slot="header">CorrectiveAction</div>
                    <el-table ref="action2" :data="list.CorrectiveAction" size="small" style="width: 90%;">
                        <el-table-column label="Action" prop="name" />
                        <el-table-column label="Responsibility" prop="responsibility" />
                        <el-table-column label="Status" prop="status" :formatter="statusFormate" />
                        <el-table-column label="DueDate" prop="dueDate" />

                    </el-table>
                </el-card>
            </el-col>
            <el-col :xs="24" :sm="24" :lg="8">
                <el-card class="card">
                    <div slot="header">attachFiles</div>
                    <el-row>
                        <div v-for="(item, index) in attachFiles.CorrectiveAction" :key="item.id">
                            <el-image :key="index" style="width: 100px; height: 100px" :src="item.url"
                                fit="fill"></el-image>
                        </div>
                    </el-row>
                    <el-row>
                        <div v-for="(item, index) in attachDocs.CorrectiveAction" :key="item.id">
                            <el-link :key="index" icon="el-icon-document" :href="item.url">{{ item.name }}</el-link>
                        </div>
                    </el-row>
                </el-card>
            </el-col>

        </el-row>
    </div>

</template>

<script>
import baseService from "@/api/base";
const defaultForm = {
    id: '',
    a3Id: '15d7702c-385a-b8de-ecf3-3a0de7b020fa',
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
    data() {
        return {
            form: Object.assign({}, defaultForm),
            generalData: [],
            list: {
                issue: [],
                containmentAction: [],
                RiskAssessment: [],
                cause: [],
                CorrectiveAction: []
            },
            attachFiles: {},
            attachDocs: {},
            category: ['issue', 'containmentAction', 'cause', 'RiskAssessment', 'CorrectiveAction'],
            issueTypes: [],
            causeTypes: [],
            causeStatus: [],
            levelOptions: [],
            url: 'https://cube.elemecdn.com/6/94/4d3ea53c084bad6931a56d5158a48jpeg.jpeg'
        }
    },
    created() {
        this.getActionTypes();
        this.getIssueTypeList();
        this.getLevelOptions();
        this.getCauseTypes();
        this.getStatus();
        setTimeout(() => {
            this.fetchData();
        }, 500);
    },
    methods: {
        fetchData() {
            this.getGeneral();
            this.category.forEach(item => {
                this.getList(item);
                this.getImages(item);
                this.getDocs(item);
            })
        },
        getGeneral() {
            this.$axios.gets('api/AAA/A3/' + this.form.a3Id).then(response => {
                this.generalData = [response];
                // console.log(response.data);
                // console.log(this.generalData);
            });

        },
        getImages(category) {
            this.$axios.gets('api/AAA/A3Attachment', { a3id: this.form.a3Id, category: category, type: 'image' }).then(response => {
                //强制页面刷新数据
                // this.list[category] = response.items;
                this.$set(this.attachFiles, category, response.items);
            });
        },
        getDocs(category) {
            this.$axios.gets('api/AAA/A3Attachment', { a3id: this.form.a3Id, category: category, type: 'doc' }).then(response => {
                //强制页面刷新数据
                // this.list[category] = response.items;
                this.$set(this.attachDocs, category, response.items);
            });
        },
        getList(category) {
            this.$axios.gets('api/AAA/' + category, { a3id: this.form.a3Id }).then(response => {
                //强制页面刷新数据
                // this.list[category] = response.items;
                this.$set(this.list, category, response.items);
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
        issueTypeFormatter(row, column, val, index) {
            // this.getIssueTypeList();
            let temp = this.issueTypes.find(item => item.value === val);
            // console.log(temp);
            return temp ? temp.label : 'undefined';
            // return this.commonFormatter(this.issueTypes, val);
        },
        getActionTypes() {
            baseService.fetchOptionsList({ name: 'actionType' }).then(
                res => {
                    this.actionTypes = res.data.items;
                }
            )
        },
        actionTypeFormate(row, column, val, index) {
            let temp = this.actionTypes.find(item => item.value === val);
            // console.log(temp);
            return temp ? temp.label : 'undefined';
        },
        getCauseTypes() {
            if (this.causeTypes.length > 0) {
                return;
            }
            baseService.fetchOptionsList({ name: 'causeTypes' }).then(
                res => {
                    this.causeTypes = res.data.items;
                }
            )

        },
        typeFormate(row, column, val, index) {
            let temp = this.causeTypes.find(item => item.value === val);
            // console.log(temp);
            return temp ? temp.label : 'undefined';
        },
        getStatus() {
            baseService.fetchOptionsList({ name: 'status' }).then(
                res => {
                    this.status = res.data.items;
                })
        },
        statusFormate(row, column, val, index) {
            let temp = this.status.find(item => item.value === val);
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
        levelFormate(row, column, val, index) {
            let temp = this.levelOptions.find(item => item.value === val);
            // console.log(temp);
            return temp ? temp.label : 'undefined';
        },


    }
}
</script>

<style lang="scss" scoped>
.dashboard-editor-container {
    padding: 16px;
    background-color: rgb(240, 242, 245);
    position: relative;



    .chart-wrapper {}

    .card {
        background: rgb(255, 255, 255);
        padding: 16px 16px 0;
        margin-bottom: 16px;
    }

    .images {
        width: 100px;
        height: 100px
    }

    .attachment-doc {
        // color: #606266;
        // display: block;
        // margin-right: 40px;
        // overflow: hidden;
        // padding-left: 4px;
        // text-overflow: ellipsis;
        // -webkit-transition: color .3s;
        // transition: color .3s;
        // white-space: nowrap;
        margin-top: 10px;
    }
}
</style>
