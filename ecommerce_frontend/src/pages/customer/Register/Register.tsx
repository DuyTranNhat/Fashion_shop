import React from 'react';
import * as Yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import { useAuth } from '~/Context/useAuth';
import { useForm } from 'react-hook-form';

export type RegisterFormsInputs = {
  email: string;
  name: string;
  password: string;
};

const validation = Yup.object().shape({
  email: Yup.string().email('Invalid email address').required('Email is required'),
  name: Yup.string().required('Username is required'),
  password: Yup.string().min(6, 'Password must be at least 6 characters').required('Password is required'),
});

const RegisterPage = () => {
  const { registerUser } = useAuth();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<RegisterFormsInputs>({ resolver: yupResolver(validation) });

  const handleRegister = (form: RegisterFormsInputs) => {
    registerUser(form);
  };

  return (
    <div className="container">
      <div className="screen">
        <div className="screen__content">
          <form className="login" onSubmit={(handleSubmit(handleRegister))}>
            <div className="login__field">
              <i className="login__icon fas fa-envelope"></i>
              <input
                type="text"
                className="login__input"
                placeholder="Email"
                {...register('email')}
              />
              {errors.email && (
                <p className="error-message">{errors.email.message}</p>
              )}
            </div>
            <div className="login__field">
              <i className="login__icon fas fa-user"></i>
              <input
                type="text"
                className="login__input"
                placeholder="User name"
                {...register('name')}
              />
              {errors.name && (
                <p className="error-message">{errors.name.message}</p>
              )}
            </div>
            <div className="login__field">
              <i className="login__icon fas fa-lock"></i>
              <input
                type="password"
                className="login__input"
                placeholder="Password"
                {...register('password')}
              />
              {errors.password && (
                <p className="error-message">{errors.password.message}</p>
              )}
            </div>
            <button type="submit" className="button login__submit">
              <span className="button__text">Register Now</span>
              <i className="button__icon fas fa-chevron-right"></i>
            </button>
          </form>
          <div className="position-absolute d-flex" style={{marginLeft: "32px"}}>
            <h3>Register via</h3>
            <div className="social-icons ml-4">
              <a href="#" className="social-login__icon fab fa-instagram"></a>
              <a href="#" className="social-login__icon fab fa-facebook"></a>
              <a href="#" className="social-login__icon fab fa-twitter"></a>
            </div>
          </div>
        </div>
        <div className="screen__background">
          <span className="screen__background__shape screen__background__shape4"></span>
          <span className="screen__background__shape screen__background__shape3"></span>
          <span className="screen__background__shape screen__background__shape2"></span>
          <span className="screen__background__shape screen__background__shape1"></span>
        </div>
      </div>
    </div>
  );
};

export default RegisterPage;
