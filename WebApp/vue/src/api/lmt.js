import request from "@/utils/request";

var service = {
  fetchProjectList(query) {
    return request({
      url: `/api/ec/projects/`,
      method: "get",
      params: query
    });
  },
  fetchCustomerList(query) {
    return request({
      url: `/api/pj/ctmGp/`,
      method: "get",
      params: query
    });
  },
};
export default service;
