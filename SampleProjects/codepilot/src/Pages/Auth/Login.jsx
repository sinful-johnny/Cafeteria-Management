import React, {useState} from "react";
import { Formik, Form, Field, ErrorMessage } from 'formik';
import * as Yup from 'yup';

//import { ReactComponent as logoSVG } from '../../../public/logo.svg';
import "./Login.css";

export function Login(){
    let validationSchema;
    let onSubmit;

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
    }

    return (
        <>
            <div className='main--login'>
                <svg className='main--cloud' width="100%" height="100%" viewBox="0 0 1440 406" fill="none" xmlns="http://www.w3.org/2000/svg">
                  <circle cx="36" cy="85" r="85" fill="#E9E4FE"/>
                  <circle cx="1414" cy="85" r="85" fill="#E9E4FE"/>
                  <circle cx="35.5" cy="424.5" r="268.5" fill="#E9E4FE"/>
                  <circle cx="662" cy="382" r="90" fill="#E9E4FE"/>
                  <circle cx="441.5" cy="487.5" r="175.5" fill="#E9E4FE"/>
                  <circle cx="803" cy="433" r="90" fill="#E9E4FE"/>
                  <circle cx="1372.5" cy="348.5" r="199.5" fill="#E9E4FE"/>
                  <circle cx="1052" cy="472" r="182" fill="#E9E4FE"/>
                </svg>

                <div className="login--form">
                    <img className="Logo--small" src='/logo.svg'></img>
                    <h2 id="LoginHeader">Login</h2>

                    <Formik
                      initialValues={initialValues}
                      validationSchema={validationSchema}
                      onSubmit={onSubmit}
                      enableReinitialize
                    >
                      {({ isSubmitting }) => (
                          <Form className="form-container">
                            <div className="LoginForm--input">
                              <label htmlFor="name">Name</label>
                              <Field type="text" id="name" name="name" className="input-field" />
                              <ErrorMessage name="name" component="div" />
                            </div>

                            <div className="LoginForm--input">
                              <label htmlFor="password">Password:</label>
                              <Field type="password" id="password" name="password" />
                              <ErrorMessage name="password" component="div" />
                            </div>

                            <div className="HelperContainer">
                                <div className="HelperContainer--RememberLogin">
                                    <label>
                                      <input
                                        type="checkbox"
                                        checked={() => {}}
                                        onChange={() => {}}
                                      />
                                      Remember Login
                                    </label>
                                </div>

                                <div className="HelperContainer--ForgotPassword">
                                    <a href="/">Forgot password?</a>
                                </div>
                            </div>

                            <div className="Form--ButtonContainer">
                              <button type="submit" disabled={isSubmitting} className="submit-button Button--Signin">Sign In</button>
                            </div>
                          </Form>
                        )}
                    </Formik>

                    <button className="AltSignInButton">
                        <img src="/google_logo.svg" />
                        <label>
                            Sign in with Google
                        </label>
                    </button>

                    <button className="AltSignInButton">
                        <img src="/facebook_logo.svg" />
                        <label>
                            Sign in with Google
                        </label>
                    </button>

                    <button className="AltSignInButton">
                        <img src="/apple_logo.svg" />
                        <label>
                            Sign in with Apple
                        </label>
                    </button>

                    <div className="RegisterHint">
                        <h3>New to codepilot? <a>Create an account</a></h3>
                    </div>
                </div>
            </div>
        </>

    );
}