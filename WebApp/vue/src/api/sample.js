import request from "@/utils/request";

const service = {
  fetchList(query) {
    return request({
      url: "",
      method: "get",
      params: query
    });
  }
};

export default service;
