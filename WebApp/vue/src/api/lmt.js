import request from "@/utils/request";

var service = {
  fetchProjectList(query) {
    return request({
      url: `/api/launch/pjtname/`,
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
