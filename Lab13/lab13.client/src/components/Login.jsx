import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { useAuth } from '../AuthContext';
import './Login.css'

const Login = () => {
    const [formData, setFormData] = useState({
        email: '',
        password: '',
    });
    const [errors, setErrors] = useState({});
    const navigate = useNavigate();
    const { login } = useAuth();

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setErrors({});
        try {
            const response = await axios.post('/api/account/login', formData);
            login(response.data);
            const userProfile = response.data.userProfile || response.data;
            localStorage.setItem('userProfile', JSON.stringify(userProfile));
            navigate('/profile');
        } catch (error) {
            console.log(error)
            if (error.response) {
                if (error.response.status === 401) {
                    setErrors({ global: 'Invalid email or password. Please try again.' });
                } else if (error.response.data?.errors) {
                    setErrors(error.response.data.errors);
                } else {
                    setErrors({ global: 'An unexpected error occurred. Please try again.' });
                }
            } else if (error.request) {
                setError('No response from server. Please check your network connection.');
            } else {
                setError('An error occurred. Please try again.');
            }
        }
    };

    return (
        <form onSubmit={handleSubmit} className="container mt-5">
            <h2>Login</h2>
            {errors.global && (
                <div className="alert alert-danger" role="alert">
                    {errors.global}
                </div>
            )}

            <div className="form-group mb-3">
                <label htmlFor="email">Email</label>
                <input
                    className={`form-control ${errors.Email ? 'is-invalid' : ''}`}
                    type="email"
                    name="email"
                    placeholder="Email"
                    value={formData.email}
                    onChange={handleChange}
                    required
                />
                {errors.Email && (
                    <div className="invalid-feedback">
                        {errors.Email.map((error, index) => (
                            <div key={index}>{error}</div> 
                        ))}
                    </div>
                )}
            </div>

            <div className="form-group mb-3">
                <label htmlFor="password">Password</label>
                <input
                    className={`form-control ${errors.Password ? 'is-invalid' : ''}`}
                    type="password"
                    name="password"
                    placeholder="Password"
                    value={formData.password}
                    onChange={handleChange}
                    required
                />
                {errors.Password && (
                    <div className="invalid-feedback">
                        {errors.Password.map((error, index) => (
                            <div key={index}>{error}</div>  
                        ))}
                    </div>
                )}
            </div>

            <button className="btn btn-primary" type="submit">Login</button>
        </form>
    );
};

export default Login;



