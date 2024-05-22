import { BrowserRouter, Route, Routes } from "react-router-dom";
import Dashboard from "../../Pages/Dashboard";
import Stations from "../../Pages/Stations";
import Settings from "../../Pages/Settings";

function AppRoutes () {
    return (
        <Routes>
            <Route path="/" element={<Dashboard />}></Route>
            <Route path="/stations" element={<Stations />}></Route>
            <Route path="/settings" element={<Settings />}></Route>
        </Routes>
    )
}

export default AppRoutes