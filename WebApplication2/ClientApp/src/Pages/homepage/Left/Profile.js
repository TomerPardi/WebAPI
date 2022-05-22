import React from "react";
import "./Left.css"


const Profile = (props) => {
  const userData = props.userData;

  return (
    <>
      <div className='profile d-flex justify-content-start'>
        <img src='default.png'></img>
        <h1 className='font-name'> {userData}</h1>
        <h1 className='font-name text-muted opacity-25'> {" @" + props.server}</h1>
      </div>
    </>
  );
};

export default Profile;
