import axios from 'axios';

const api = axios.create({
    baseUrl: 'https://localhost:7172/api'
});

api.interceptors.request.use((config) => {
    const token = localStorage.getItem('token');
    if(token){
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

export default api;