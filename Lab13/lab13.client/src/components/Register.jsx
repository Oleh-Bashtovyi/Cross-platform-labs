import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { useAuth } from '../AuthContext';
import './Register.css';

const Register = () => {
    const [formData, setFormData] = useState({
        userName: '',
        fullName: '',
        phoneNumber: '',
        email: '',
        password: '',
        confirmPassword: '',
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
            // Надсилаємо дані для реєстрації
            await axios.post('/api/account/register', formData);

            // Після успішної реєстрації одразу виконуємо вхід
            const loginData = {
                email: formData.email,
                password: formData.password,
            };
            const response = await axios.post('/api/account/login', loginData);

            login(response.data);
            const userProfile = response.data.userProfile || response.data;
            localStorage.setItem('userProfile', JSON.stringify(userProfile));

            navigate('/profile');
        } catch (error) {
            if (error.response) {
                if (error.response.data?.errors) {
                    setErrors(error.response.data.errors);
                } else {
                    setErrors({ global: 'An unexpected error occurred. Please try again.' });
                }
            } else if (error.request) {
                setErrors({ global: 'No response from server. Please check your network connection.' });
            } else {
                setErrors({ global: 'An error occurred. Please try again.' });
            }
        }
    };

    return (
        <form onSubmit={handleSubmit} className="container mt-5">
            <h2>Register</h2>
            {errors.global && (
                <div className="alert alert-danger" role="alert">
                    {errors.global}
                </div>
            )}

            <div className="form-group mb-3">
                <label htmlFor="userName">Username</label>
                <input
                    className={`form-control ${errors.UserName ? 'is-invalid' : ''}`}
                    type="text"
                    name="userName"
                    placeholder="Username"
                    value={formData.userName}
                    onChange={handleChange}
                    required
                />
                {errors.UserName && (
                    <div className="invalid-feedback">
                        {errors.UserName.map((error, index) => (
                            <div key={index}>{error}</div>
                        ))}
                    </div>
                )}
            </div>

            <div className="form-group mb-3">
                <label htmlFor="fullName">Full Name</label>
                <input
                    className={`form-control ${errors.FullName ? 'is-invalid' : ''}`}
                    type="text"
                    name="fullName"
                    placeholder="Full Name"
                    value={formData.fullName}
                    onChange={handleChange}
                    required
                />
                {errors.FullName && (
                    <div className="invalid-feedback">
                        {errors.FullName.map((error, index) => (
                            <div key={index}>{error}</div>
                        ))}
                    </div>
                )}
            </div>

            <div className="form-group mb-3">
                <label htmlFor="phoneNumber">Phone</label>
                <input
                    className={`form-control ${errors.PhoneNumber ? 'is-invalid' : ''}`}
                    type="tel"
                    name="phoneNumber"
                    placeholder="Phone"
                    value={formData.phoneNumber}
                    onChange={handleChange}
                    required
                />
                {errors.PhoneNumber && (
                    <div className="invalid-feedback">
                        {errors.PhoneNumber.map((error, index) => (
                            <div key={index}>{error}</div>
                        ))}
                    </div>
                )}
            </div>

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

            <div className="form-group mb-3">
                <label htmlFor="confirmPassword">Confirm Password</label>
                <input
                    className={`form-control ${errors.ConfirmPassword ? 'is-invalid' : ''}`}
                    type="password"
                    name="confirmPassword"
                    placeholder="Confirm Password"
                    value={formData.confirmPassword}
                    onChange={handleChange}
                    required
                />
                {errors.ConfirmPassword && (
                    <div className="invalid-feedback">
                        {errors.ConfirmPassword.map((error, index) => (
                            <div key={index}>{error}</div>
                        ))}
                    </div>
                )}
            </div>

            <button className="btn btn-primary" type="submit">Register</button>
        </form>
    );
};

export default Register;

























/*import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { useAuth } from '../AuthContext';

const Register = () => {
    const [formData, setFormData] = useState({
        userName: '',
        fullName: '',
        phoneNumber: '',
        email: '',
        password: '',
        confirmPassword: '',
    });
    const [errors, setErrors] = useState({});
    const { login } = useAuth();
    const navigate = useNavigate();

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = async (e) => {
        console.log('Sending form...');
        e.preventDefault();
        try {
            console.log(formData);

            var registerResponse = await axios.post('/api/account/register', formData);
            setErrors({});

            console.log('Register response:')
            console.log(registerResponse)

            const loginData = {
                email: formData.email,
                password: formData.password,
            };
            const response = await axios.post('/api/account/login', loginData);

            console.log(JSON.stringify(response));

            login(response.data);
            const userProfile = response.data.userProfile || response.data;
            localStorage.setItem('userProfile', JSON.stringify(userProfile));

            navigate('/profile');
        } catch (error) {
            console.log(error);

            if (error.response && error.response.data) {
                console.error('Error response:', error.response.data);
                if (error.response.data.errors) {
                    setErrors(error.response.data.errors);
                } else {
                    alert('Error: ' + JSON.stringify(error.response.data));
                }
            } else {
                alert('Registration failed');
            }
        }
    };

    return (
        <form onSubmit={handleSubmit} className="container mt-5">
            <h2>Register</h2>
            <div className="form-group mb-3">
                <label htmlFor="userName" className="form-label">Username</label>
                <input
                    className={`form-control ${errors.UserName ? 'is-invalid' : ''}`}
                    type="text"
                    name="userName"
                    placeholder="Username"
                    value={formData.userName}
                    onChange={handleChange}
                    required
                />
                {errors.UserName && <div className="invalid-feedback">{errors.UserName}</div>}
            </div>
            <div className="form-group mb-3">
                <label htmlFor="fullName" className="form-label">Full Name</label>
                <input
                    className={`form-control ${errors.FullName ? 'is-invalid' : ''}`}
                    type="text"
                    name="fullName"
                    placeholder="Full Name"
                    value={formData.fullName}
                    onChange={handleChange}
                    required
                />
                {errors.FullName && <div className="invalid-feedback">{errors.FullName}</div>}
            </div>
            <div className="form-group mb-3">
                <label htmlFor="phoneNumber" className="form-label">Phone</label>
                <input
                    className={`form-control ${errors.PhoneNumber ? 'is-invalid' : ''}`}
                    type="tel"
                    name="phoneNumber"
                    placeholder="Phone"
                    value={formData.phoneNumber}
                    onChange={handleChange}
                    required
                />
                {errors.PhoneNumber && <div className="invalid-feedback">{errors.PhoneNumber}</div>}
            </div>
            <div className="form-group mb-3">
                <label htmlFor="email" className="form-label">Email</label>
                <input
                    className={`form-control ${errors.Email ? 'is-invalid' : ''}`}
                    type="email"
                    name="email"
                    placeholder="Email"
                    value={formData.email}
                    onChange={handleChange}
                    required
                />
                {errors.Email && <div className="invalid-feedback">{errors.Email}</div>}
            </div>
            <div className="form-group mb-3">
                <label htmlFor="password" className="form-label">Password</label>
                <input
                    className={`form-control ${errors.Password ? 'is-invalid' : ''}`}
                    type="password"
                    name="password"
                    placeholder="Password"
                    value={formData.password}
                    onChange={handleChange}
                    required
                />
                {errors.Password && <div className="invalid-feedback">{errors.Password}</div>}
            </div>
            <div className="form-group mb-3">
                <label htmlFor="confirmPassword" className="form-label">Confirm Password</label>
                <input
                    className={`form-control ${errors.ConfirmPassword ? 'is-invalid' : ''}`}
                    type="password"
                    name="confirmPassword"
                    placeholder="Confirm Password"
                    value={formData.confirmPassword}
                    onChange={handleChange}
                    required
                />
                {errors.ConfirmPassword && <div className="invalid-feedback">{errors.ConfirmPassword}</div>}
            </div>
            <button className="btn btn-primary" type="submit">Register</button>
        </form>
    );
};

export default Register;*/