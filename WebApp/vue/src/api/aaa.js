import request from "@/utils/request";

var service = {
  fetchList(query) {
    return request({
      url: `/api/AAA/A3`,
      method: "get",
      params: query
    });
  },
  fetchSingle(id) {
    return request({
      url: `/api/AAA/A3/${id}`,
      method: "get"
    });
  },
  post(data) {
    return request({
      url: `/api/AAA/A3/data-post`,
      method: "post",
      data,
    //   headers: {
    //     "Content-Type": "application/x-www-form-urlencoded;charset=UTF-8;"
    //   }
    });
  },
  share(data) {
    return request({
      url: `/api/AAA/A3/share`,
      method: "post",
      data,    
    });
  }
};

export default service;
