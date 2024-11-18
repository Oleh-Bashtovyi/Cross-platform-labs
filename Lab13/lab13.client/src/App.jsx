import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { useState } from 'react';
import Home from './components/Home';
import Register from './components/Register';
import Login from './components/Login';
import Profile from './components/Profile';
import LabPage from './components/LabPage';
//import Head from './components/Head';
import Header from './components/Header';

function App() {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [userName, setUserName] = useState('');

    return (
        <Router>
{/*            <Head />*/}
            <Header isAuthenticated={isAuthenticated} userName={userName} />
            <div class="container">
                <main role="main" class="pb-3">
                    <Routes>
                        <Route path="/" element={<Home />} />
                        <Route path="/register" element={<Register />} />
                        <Route path="/login" element={<Login setIsAuthenticated={setIsAuthenticated} setUserName={setUserName} />} />
                        <Route path="/profile" element={<Profile />} />
                        <Route path="/lab1" element={<LabPage labNumber="1" />} />
                        <Route path="/lab2" element={<LabPage labNumber="2" />} />
                        <Route path="/lab3" element={<LabPage labNumber="3" />} />
                    </Routes>
                </main>
            </div>
        </Router>
    );
}

export default App;





//import React from 'react';
//import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
//import 'bootstrap/dist/css/bootstrap.min.css';
//import Layout from './components/Layout';
//import Home from './components/Home';
//import Profile from './components/Profile';
//import Login from './components/Login';
//import Register from './components/Register';

//const App = () => (
//    <Router>
//        <Layout>
//            <Routes>
//                <Route path="/" element={<Home />} />
//                <Route path="/profile" element={<Profile />} />
//                <Route path="/login" element={<Login />} />
//                <Route path="/register" element={<Register />} />
//                {/* Інші маршрути */}
//            </Routes>
//        </Layout>
//    </Router>
//);

//export default App;





/*import { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';

function App() {
    const [forecasts, setForecasts] = useState();

    useEffect(() => {
        populateWeatherData();
    }, []);

    const contents = forecasts === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                </tr>
            </thead>
            <tbody>
                {forecasts.map(forecast =>
                    <tr key={forecast.date}>
                        <td>{forecast.date}</td>
                        <td>{forecast.temperatureC}</td>
                        <td>{forecast.temperatureF}</td>
                        <td>{forecast.summary}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tableLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );
    
    async function populateWeatherData() {
        const response = await fetch('weatherforecast');
        const data = await response.json();
        setForecasts(data);
    }
}

export default App;*/