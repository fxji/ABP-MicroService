<template>
    <el-select ref="select" v-model="hValue" :allow-create="allowCreate" :autocomplete="autocomplete"
        :automatic-dropdown="automaticDropdown" :clearable="clearable" :collapse-tags="collapseTags"
        :default-first-option="defaultFirstOption" :disabled="disabled" :filter-method="filterMethod"
        :filterable="filterable" :loading="loading" :loading-text="loadingText" :multiple="multiple"
        :multiple-limit="multipleLimit" :name="name" :no-match-text="noMatchText" :no-data-text="noDataText"
        :placeholder="placeholder" :popper-class="popperClass" :popper-append-to-body="popperAppendToBody" :remote="remote"
        :remote-method="remoteMethod" :reserve-keyword="reserveKeyword" :size="size" :key="poperKeyValue"
        :value-key="valueKey" @blur="handleBlur" @change="handleChange" @clear="handleClear" @focus="handleFocus"
        @remove-tag="handleRemoveTag" @visible-change="handleVisibleChange">
        <slot name="prefix" slot="prefix"></slot>
        <slot name="option-content">
            <template v-for="(item, index) in dataSource">
                <el-option-group v-if="item[hProps.options] &&
                    item[hProps.options].length > 0 &&
                    !selectSpecial
                    " :key="index" :label="item[hProps.label]" :disabled="item[hProps.disabled]">
                    <el-option v-for="(option, subIndex) in item[hProps.options]" :key="subIndex"
                        :label="option[hProps.label]" :value="option[hProps.value]"
                        :disabled="option[hProps.disabled]"></el-option>
                </el-option-group>
                <el-option v-else-if="!item[hProps.options] && !selectSpecial" :key="index + 'si'"
                    :label="item[hProps.label]" :value="item[hProps.value]" :disabled="item[hProps.disabled]"></el-option>
                <el-option v-else-if="!item[hProps.options] && selectSpecial" :key="index + 'sp'"
                    :label="`${item[hProps.label]}(${item[selectSpecial]})`" :value="item[hProps.value]"
                    :disabled="item[hProps.disabled]"></el-option>
            </template>
        </slot>
    </el-select>
</template>
  
<script>
export default {
    name: 'HSelect',
    props: {
        allowCreate: {
            type: Boolean,
            default: false
        },
        selectSpecial: {
            type: String,
            default: ''
        },
        autocomplete: String,
        automaticDropdown: {
            type: Boolean,
            default: false
        },
        clearable: {
            type: Boolean,
            default: true
        },
        collapseTags: {
            type: Boolean,
            default: false
        },
        dataSource: Array,
        defaultFirstOption: {
            type: Boolean,
            default: false
        },
        disabled: {
            type: Boolean,
            default: false
        },
        filterMethod: Function,
        filterable: {
            type: Boolean,
            default: true
        },
        loading: {
            type: Boolean,
            default: false
        },
        loadingText: String,
        multiple: {
            type: Boolean,
            default: false
        },
        multipleLimit: Number,
        name: String,
        noMatchText: String,
        noDataText: String,
        placeholder: String,
        popperClass: {
            type: String,
            default: 'select-default'
        },
        popperAppendToBody: {
            type: Boolean,
            default: true
        },
        remote: {
            type: Boolean,
            default: false
        },
        remoteMethod: Function,
        reserveKeyword: {
            type: Boolean,
            default: false
        },
        size: {
            type: String,
            validator(value) {
                return ['medium', 'small', 'mini'].indexOf(value) !== -1
            }
        },
        value: {
            type: [String, Number, Array, Boolean],
            required: true
        },
        valueKey: String,
        props: {
            type: Object,
            default() {
                return {}
            }
        },
        keyValue: {
            type: String,
            default: 'select-single'
        },
        align: {
            type: String,
            default: 'center'
        }
    },
    data() {
        return {
            poperKeyValue: ''
        }
    },
    computed: {
        hValue: {
            get() {
                let value = null
                if (this.multiple) {
                    value = []
                    if (this.value instanceof Array) {
                        this.value.forEach(key => {
                            if (this.checkValueExisting(key)) {
                                value.push(key)
                            }
                        })
                    }
                } else {
                    value = ''
                    if (this.checkValueExisting(this.value)) {
                        value = this.value
                    }
                }
                return value
            },
            set(value) {
                this.$emit('input', value)
            }
        },
        hProps() {
            return {
                label: 'name',
                value: 'id',
                disabled: 'disabled',
                options: 'options',
                ...this.props
            }
        }
    },
    watch: {
        keyValue(val) {
            this.poperKeyValue = val
        }
    },
    methods: {
        checkValueExisting(value) {
            if (this.allowCreate) {
                return value
            } else {
                if (this.dataSource instanceof Array) {
                    let index = this.dataSource.findIndex(
                        item => item[this.hProps.value] === value,
                        this
                    )
                    return index > -1
                }
                return false
            }
        },
        handleBlur(event) {
            this.$emit('blur', event)
        },
        handleChange(value) {
            this.$emit('change', value)
        },
        handleClear() {
            this.$emit('clear')
        },
        handleFocus(event) {
            this.$emit('focus', event)
        },
        handleRemoveTag(tag) {
            this.$emit('remove-tag', tag)
        },
        handleVisibleChange(visible) {
            this.$emit('visible-change', visible)
        },
        focus() {
            this.$refs.select.focus()
        },
        blur() {
            this.$refs.select.blur()
        }
    }
}
</script>
  
<style lang="scss" scoped></style>
  
  