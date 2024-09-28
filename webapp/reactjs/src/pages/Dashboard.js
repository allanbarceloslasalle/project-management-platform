import React, {useEffect, useState} from 'react';
import api from './../api/axiosConfig';

const Dashboard = () => {

    const handleLogout = () => {
        localStorage.removeItem('token');
        window.location.href = '/login';
    };

    const [data, setData] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await api.get('https://localhost:7172/api/project');
                setData(response.data);
            } catch (error) {
                console.error("Errot fetch projects", error);
                
            }
        };
        fetchData();
    }, [])

    return (
        <>
        <h2>Dashboard</h2>
        <p>This is dashboard</p>
        <button onClick={handleLogout}>Logout</button>
        {data ? <pre>{data}</pre> : <p>Loading ...</p>}
        </>
    )
};

export default Dashboard;