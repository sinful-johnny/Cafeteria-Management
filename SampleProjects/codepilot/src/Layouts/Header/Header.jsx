import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faRightToBracket } from "@fortawesome/free-solid-svg-icons";
import { useNavigate } from "react-router-dom";

import "./Header.css";

const Header = () => {
  const navigate = useNavigate();

  const logOut = () => {
    localStorage.clear();
    navigate("/");
  };
  return (
    <div className="admin-header">
      <div className="admin-logo">Admin</div>
      <button className="admin-head-logout-btn" onClick={logOut}>
        LOGOUT
        <FontAwesomeIcon icon={faRightToBracket} className="head-login-ico" />
      </button>
    </div>
  );
};

export default Header;
