import { BrowserRouter, Route, Routes } from "react-router-dom";
import Login from "../../Pages/Login";
import Register from "../../Pages/Register";
import Dashboard from "../../Pages/Dashboard";
import Stations from "../../Pages/Stations";
import Settings from "../../Pages/Settings";

function AppRoutes () {
    return (
        <Routes>
            <Route path="/" element={<Login />}></Route>
            <Route path="/register" element={<Register />}></Route>
            <Route path="/dashboard" element={<Dashboard />}></Route>
            <Route path="/stations" element={<Stations />}></Route>
            <Route path="/settings" element={<Settings />}></Route>
        </Routes>
    )
}

export default AppRoutes