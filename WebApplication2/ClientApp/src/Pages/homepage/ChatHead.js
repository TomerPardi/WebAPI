import React, { useContext } from "react";
import AppContext from "../../AppContext";
import defaultImage from "./default.jpg";

const Chathead = (props) => {
  const sharedContext = useContext(AppContext);
  // TODO: discuss how to take care of photo? it is not specefied in API
  // const contactData = context.userData; ??????
  // const photo = contactData[props.activeContact].photo; ??????
  if (props.activeContact === "none") {
    return <div>placeholder</div>;
  } else {
    return (
      <div className='chat-head'>
      {/* TODO: again, what to do with photos? */}
        <img
          src="default.jpg"
          alt={defaultImage}
        ></img>
        <div className='chat-name'>
          <h1 className='font-name'>
            {props.activeContact.name}
          </h1>
          {/* <p className="font-online"></p> */}
        </div>
      </div>
    );
  }
};

export default Chathead;
