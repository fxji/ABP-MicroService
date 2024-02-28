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
      url: `/api/base/orgs/loadOrgs/${id}`,
      method: "get",
    });
  },
  fetchUser(id) {
    return request({
      url: `/api/base/user/${id}`,
      method: "get"
    });
  },

  fetchOptionsList(query) {
    return request({
      url: `/api/base/dictDetails/list`,
      method: "get",
      params: query
    });
  },
  fetchUserList(query) {
    return request({
      url: `/api/base/user`,
      method: "get",
      params: query
    });
  }
};

export default service;
