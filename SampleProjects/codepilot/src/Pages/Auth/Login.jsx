import React, {useState} from "react";
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import { useNavigate } from "react-router-dom";

//import { ReactComponent as logoSVG } from '../../../public/logo.svg';
import "./Login.css";
import axios from "axios";

export function Login(){
    let validationSchema;
    let onSubmit;
    let navigate = useNavigate();

    const [initialValues, setInitialValues] = useState({
        name: "" ,
        password: "",
      })
    
    validationSchema = Yup.object({
        name: Yup.string().required('Required'),
        password: Yup.string()
        .min(8, 'Password must be at least 8 characters long')
        .required('Password is required'),
    });

    onSubmit = (values) => {
        console.log(values);
        axios.post("https://localhost:7233/api/account/login", {emailAddress: values.name, password: values.password})
        .then(response => {
            sessionStorage.setItem("authToken", response.data.token);
            axios.defaults.headers.common['Authorization'] = `Bearer ${response.data.token}`;
            console.log(response.data);
            navigate("/canvas");
        }).catch(error => {
            console.log("Log in error",error);
            if(error.response.status === 401){
                console.log("Username not found or password incorrect");
            }
        })
    }

    //THIS DOES NOT WORK WITH ERROR MESSAGE AND I HAVE NO IDEA WHY
    function FloatingLabelInput({ label, name, type = "text" }) {
        const [isFocused, setIsFocused] = useState(false);
    
        const handleFocus = () => setIsFocused(true);
        const handleBlur = (event) => {
            setIsFocused(event.target.value !== ""); // Remain focused if there's content
        };
    
        return (
            <div className="LoginForm--input">
                <Field
                    type={type}
                    id={name}
                    name={name}
                    placeholder=" " // Empty placeholder for CSS floating effect
                    className={`input-field ${isFocused ? "focused" : ""}`}
                    onFocus={handleFocus}
                    onBlur={handleBlur}
                    required
                />
                <ErrorMessage name={name} component="div" className="error" />
                <label className={`floating-label ${isFocused ? "focused" : ""}`}>
                    {label}
                </label>
            </div>
        );
    } 

    return (
        <>
            <div className='main--login'>
                <h2 className="login-text">Your shift has started; log in and give it your best!</h2>

                <svg className='main--cloud' width="100%" height="100%" viewBox="0 0 1440 406" fill="none" xmlns="http://www.w3.org/2000/svg">
                  <circle cx="521" cy="70" r="160" fill="#BB9370" opacity={0.56}/>
                  <circle cx="400" cy="200" r="50" fill="#BB9370" opacity={0.56}/>
                  <circle cx="300" cy="-70" r="40" fill="#BB9370" opacity={0.56}/>
                </svg>

                <div className="login--form">
                    <h1 id="LoginHeader">Welcome!</h1>

                    <Formik
                      initialValues={initialValues}
                      validationSchema={validationSchema}
                      onSubmit={onSubmit}
                      enableReinitialize
                    >
                      {({ isSubmitting }) => (
                          <Form className="form-container">
                            <FloatingLabelInput
                                label="Email"
                                name="name"
                                type="text"
                            />
                            
                            <FloatingLabelInput
                                label="Password"
                                name="password"
                                type="password"
                            />

                            <div className="Form--ButtonContainer">
                              <button type="submit" disabled={isSubmitting} className="submit-button Button--Signin">Login</button>
                            </div>
                          </Form>
                        )}
                    </Formik>
                </div>
            </div>
        </>

    );
}