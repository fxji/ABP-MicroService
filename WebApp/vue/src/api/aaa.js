import request from "@/axios";

var service = {
  fetchList(query) {
    return request.gets(`/api/AAA/A3`, query);
  },
  fetchSingle(id) {
    return request.gets(`/api/AAA/A3/${id}`);
  },
  post(data) {
    return request.posts(`/api/AAA/A3/data-post`, data);
  },
  share(data) {
    return request.posts(`/api/AAA/A3/share`, data);
  },
  export(id) {
    return request.gets(`/api/AAA/A3/export/${id}`);
  }
};

export default service;
