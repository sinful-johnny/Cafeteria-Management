import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faRightToBracket } from "@fortawesome/free-solid-svg-icons";
import { useNavigate } from "react-router-dom";
import React, {useRef,useEffect,useState} from "react";
import logo from './Logo.png';
import userImg from './Rectangle.png'
import logoutSymbol from './Logout.png'

import "./Header.css";

const Header = ({selectedPage, setSelectedPage, setIsTableMenu, save, isSaved}) => {
  const navigate = useNavigate();
  const [isDropdown, setDropdown] = useState(false);

  useEffect(() => {
    //console.log(selectedPage);
  },[])
  //const [selectedPage, setSelectedPage] = useState("Table");
  // const logOut = () => {
  //   localStorage.clear();
  //   navigate("/");
  // };

  return (
    <div className="header">
      <div className="logo-and-nav">
        <img src={logo} className="logo-image"  alt="LOGO" />
        <div className="logo-text">
          <h3>Morning coffee</h3>
          <div className="function-selection">
            <div className="nav-buttons">
                <button
                    className={`nav-button ${selectedPage === "Table" ? "selected" : ""}`}
                    onClick={() => {
                        setSelectedPage("Table");
                        setIsTableMenu(true);
                    }}
                >
                    Table
                </button>
                <button
                    className={`nav-button ${selectedPage === "Order" ? "selected" : ""}`}
                    onClick={() => {
                        setSelectedPage("Order");
                        setIsTableMenu(false);
                    }}
                >
                    Order
                </button>
            </div>
            {/* Save Button */}
            <button
                  className={`save-button-${isSaved ? "Saved" : "Unsaved"}`}
                  onClick={() => {
                    if (!isSaved) {
                      save();
                    }
                  }}
                >
                  {isSaved ? "Saved" : "Unsaved"}
                </button>
            <div
                className="underline"
                style={{
                    left: selectedPage === "Table" ? "0px" : "64px",
                }}
            ></div>
        </div>
        </div>
      </div>
      <button 
        className="user-toggle-menu"
        onClick = {() => setDropdown((prev) => !prev)}   
      >
        <img className="user-image" src={userImg} alt="Avatar" />
      </button>
      <div className={`dropdown-menu ${isDropdown ? "dropdown-visible" : ""}`}>
        <ul>
          <li>
            <img src={logoutSymbol} alt="logout symbol"/>
            <button className="logout" onClick={() => navigate("/")}> Logout </button>
          </li>
        </ul>
      </div>
    </div>
  );
};
export default Header;
