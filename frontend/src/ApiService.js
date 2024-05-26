// src/services/apiService.js
import axios from 'axios';

const API_BASE_URL = 'http://localhost:5205'; // Update with your API base URL

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = token; // No 'Bearer' prefix
    }
    return config;
  },
  (error) => Promise.reject(error)
);

api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response) {
      console.error('API Error:', error.response.data);
    } else if (error.request) {
      console.error('Network Error:', error.request);
    } else {
      console.error('Error:', error.message);
    }
    return Promise.reject(error);
  }
);

const apiService = {
  get(endpoint, params) {
    return api.get(endpoint, { params });
  },
  post(endpoint, data) {
    return api.post(endpoint, data);
  },
  put(endpoint, data) {
    return api.put(endpoint, data);
  },
  delete(endpoint) {
    return api.delete(endpoint);
  },
  async login(username, password) {
    const response = await api.post('/User/login', { username, password });
      if (response.data.success) {
          localStorage.setItem('token', response.data.data.token);
          localStorage.setItem('username', response.data.data.user.username);
      }
      return response;
  },
  logout() {
    localStorage.removeItem('token');
  }
};

export default apiService;
