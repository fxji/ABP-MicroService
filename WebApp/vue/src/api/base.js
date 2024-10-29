import request from "@/axios";

var service = {
  fetchOrgNodesList(query) {
    return request.gets(`/api/base/orgs/loadNodes`, query);
  },

  fetchOrgs(id) {
    return request.gets(`/api/base/orgs/loadOrgs/${id}`);
  },
  fetchUser(id) {
    return request.gets(`/api/base/user/${id}`);
  },

  fetchOptionsList(query) {
    return request.gets(`/api/base/dictDetails/list`, query);
  },
  fetchUserList(query) {
    return request.gets(`/api/base/user`, query);
  }
};

export default service;
