import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
//import './Profile.css';
import { useAuth } from '../AuthContext';

const Profile = () => {
    const [profile, setProfile] = useState(null);
    const { isAuthenticated, logout } = useAuth();
    const navigate = useNavigate();

/*    const handleLogout = () => {
        logout();
        navigate('/');
    };*/



    useEffect(() => {
        // Завантаження профілю з локального сховища
        const storedProfile = localStorage.getItem('userProfile');

        console.log(storedProfile);

        if (storedProfile) {
            setProfile(JSON.parse(storedProfile));
        } else {
            navigate('/login');
        }
    }, [navigate]);

    // Логіка виходу
    const handleLogout = async () => {
        try {
            await axios.post('/api/account/logout');  // Запит на сервер для виходу
            logout();
            navigate('/');
        } catch (error) {
            console.error('Logout error:', error);
            alert('Logout failed: ' + (error.response?.data?.Error || error.message));
        }
    };

    if (!profile) {
        return <p>Loading profile...</p>;
    }

    return (
        <div className="container mt-5">
            <div className="card" style={{ maxWidth: '600px', margin: 'auto' }}>
                <div className="card-header text-center">
                    <h3>Профіль користувача</h3>
                </div>
                <div className="card-body text-center">
                    {/* Зображення профілю */}
                    <img
                        src={profile.profileImage}
                        alt="Profile Image"
                        className="rounded-circle mb-3"
                        style={{ width: '120px', height: '120px', objectFit: 'cover' }}
                    />
                    {/* Інформація користувача */}
                    <h4 className="card-title">{profile.fullName}</h4>
                    <p className="text-muted">{profile.userName}</p>

                    <div className="mt-4 text-start">
                        <p><strong>Email:</strong> {profile.email}</p>
                        <p><strong>Номер телефону:</strong> {profile.phoneNumber}</p>
                    </div>
                </div>
                <div className="card-footer text-center">
                    {/* Кнопка виходу */}
                    <button onClick={handleLogout} className="btn btn-danger">Logout</button>
                </div>
            </div>
        </div>
    );
};

export default Profile;
