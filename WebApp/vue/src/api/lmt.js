import request from "@/axios";

var service = {
  fetchProjectList(query) {
    return request.gets(`/api/launch/pjtname/`, query);
  },
  fetchCustomerList(query) {
    return request.gets(`/api/pj/ctmGp/`, query);
  }
};
export default service;
