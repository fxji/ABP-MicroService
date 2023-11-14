<template>
    <div>
        <el-card>
            <el-steps :active="active" finish-status="success" align-center>
                <el-step title="BasicInfo"></el-step>
                <el-step title="Issues"></el-step>
                <el-step title="Containment Actions"></el-step>
                <el-step title="Risk Assessment"></el-step>
                <el-step title="Causes"></el-step>
                <el-step title="Corrective Actions"></el-step>
            </el-steps>
            <!-- <A3BasicInfo></A3BasicInfo> -->
        </el-card>
        <el-card>
            <Issue></Issue>
        </el-card>
    </div>
</template>
<script>
import A3BasicInfo from '@/views/a3-report-management/components/a3-basic-info'
import Issue from '@/views/a3/issue/index.vue'

export default {
    name: 'A3Report',
    components: { A3BasicInfo, Issue },
    props: {
        isEdit: {
            type: Boolean,
            default: false,
        }
    },
    data() {
        return {
            active: 0,
            showStatus: [true, false, false, false]
        }
    },
    methods: {
        hideAll() {
            for (let index = 0; index < this.showStatus.length; index++) {
                this.showStatus[index] = false;
            }
        },
        prevStep() {
            if (this.active > 0 && this.active < this.showStatus.length) {
                this.active--;
                this.hideAll();
                this.showStatus[this.active] = true;
            }
        },
        nextStep() {
            if (this.active < this.showStatus.length - 1) {
                this.active++;
                this.hideAll();
                this.showStatus[this.active] = true;
            }
        }
    }
}
</script>