
// v-loadmore: 用于在element-ui的select下拉框加上滚动到底事件监听
// scrollHeight 获取元素内容高度(只读)
// scrollTop 获取或者设置元素的偏移值,常用于, 计算滚动条的位置, 当一个元素的容器没有产生垂直方向的滚动条, 那它的scrollTop的值默认为0.
// clientHeight 读取元素的可见高度(只读)
// 如果元素滚动到底, 下面等式返回true, 没有则返回false
// scrollHeight - scrollTop === clientHeight


export default {
  install(Vue) {
    //滚动加载
    Vue.directive("loadMore", {
      bind(el, binding) {
        //获取element， 定义scroll
        let select_dom = el.querySelector(
          ".el-select-dropdown .el-select-dropdown__wrap"
        );
        select_dom.addEventListener("scroll", function() {
          let height = this.scrollHeight - this.scrollTop <= this.clientHeight;
          if (height) {
            binding.value();
          }
        });
      }
    });
  }
};
