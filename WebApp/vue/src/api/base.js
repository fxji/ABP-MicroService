import request from "@/utils/request";

var service = {
  fetchOrgNodesList(query) {
    return request({
      url: `/api/base/orgs/loadNodes`,
      method: "get",
      params: query
    });
  },

  fetchOrgs(id) {
    return request({
      url: "/api/base/orgs/loadOrgs",
      method: "get",
      params: { id }
    });
  },

  fetchOptionsList(query) {
    return request({
      url: `/api/base/dictDetails/list`,
      method: "get",
      params: query
    });
  },
  
};

export default service;
