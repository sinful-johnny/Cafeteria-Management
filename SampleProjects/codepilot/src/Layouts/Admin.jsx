import { Outlet } from "react-router-dom";
import Header from "./Header/Header";
import Navbar from "./Navbar/Navbar";
import "../App.css";

const Admin = () => {
  const bodyStyle = {
    display: "flex",
  };
  const contentStyle = {
    width: "75%",
    padding: "20px",
    backgroundColor: "white",
  };
  return (
    <div className="Global--Container">
      <Header />
      <div className="BodyStyle">
        <Navbar />
        <div className="ContentStyle">
          <Outlet />
        </div>
      </div>
    </div>
  );
};

export default Admin;