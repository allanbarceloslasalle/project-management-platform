import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';


const Login = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await axios.post('https://localhost:7172/api/auth/login', { email, password });
            localStorage.setItem('token', response.data.token);
            navigate('/dashboard');
        } catch (error) {
            setError('Invalid Credentials');
        }
    };

    return (
        <>
            <h2>Login</h2>
            <form onSubmit={handleSubmit}>

                <div className="mb-3">
                    <label htmlFor="email" className="form-label">Email address</label>
                    <input type='email' className="form-control" value={email} onChange={(e) => setEmail(e.target.value)} placeholder='Email' required />
                </div>

                <div className="mb-3">
                    <label htmlFor="email" className="form-label">Password</label>
                    <input type='password' className="form-control" value={password} onChange={(e) => setPassword(e.target.value)} placeholder='Password' required />
                </div>

                <button type='submit' className="btn btn-primary">Login</button>
            </form>
            {error && <p>{error}</p>}


        </>
    );

};


export default Login;
