import React, { useEffect, useState } from "react";
import MessageInput from "./Right/MessageInput";
import Chathead from "./Right/ChatHead";
import Chatwindow from "./Right/ChatWindow";
import Contactslist from "./Left/ContactsList";
import "./Homepage.css";
import Profile from "./Left/Profile";
import AppContext from "../../AppContext"
import OutsideAlerter from "../useOutside";
import axios from "axios";


export default function Homepage(props) {
  let sharedContext = React.useContext(AppContext);
  const [messages, setMessages] = useState([]);
  const [contacts, setContacts] = useState([]);
  const [active, setActive] = useState("none");
  const [activeInfo, setActiveInfo] = useState("none");

  useEffect(() => {
    const getContacts = async () => {
      // we receive json from server via api`
      const result = await axios.get(`https://${sharedContext.hostname}:7066/api/contacts`, {
        withCredentials: true,
      });
      setContacts(result.data);
    };
    getContacts();
    props.setChanged(false);
  }, [props.changed])


  useEffect(() => {

    const getMessages = async () => {
      const result = await axios.get(
        `https://${sharedContext.hostname}:7066/api/contacts/${active}/messages`,
        {
          withCredentials: true,
        }
      );
      setMessages(result.data);

      const result2 = await axios.get(
        `https://${sharedContext.hostname}:7066/api/contacts/${active}`,
        {
          withCredentials: true,
        }
      );
      setActiveInfo(result2.data);
    };

    if (active !== "none") getMessages();
    props.setChanged(false);
  }, [props.changed, active]);

  function conditionalRight() {
    if (active === "none") {
      return (
        <div
          style={{ height: "100%", background: "#99eda1" }}
          className='d-flex align-items-center flex-column'
        >
          <img
            style={{ height: "75%", maxWidth: "100%" }}
            src="static-airplane.png"
            alt='img'
          ></img>
          <div className='fs-5'>
            <em>A new way to communicate with your friends!</em>
          </div>
        </div>
      );
    }

    return (
      <>
        <Chathead activeContact={activeInfo} />
        <Chatwindow
          messages={messages}
          setter={setMessages}
          activeContact={active}
        />
        <MessageInput
          messages={messages}
          setter={props.setChanged}
          activeInfo={activeInfo}
        />
      </>
    );
  }

  return (
    <>
      <OutsideAlerter setter={setActive}>
        <section className='left'>
          {/* nickname and user's image will be here */}
          <Profile
            userData={sharedContext.currentUser}
            server={sharedContext.server}
          />
          {/* contacts list and search bar for contacts will be here */}
          <Contactslist
            setter={props.setChanged}
            setActive={setActive}
            contactsList={contacts}
          />
        </section>

        <section className='right'>{conditionalRight()}</section>
      </OutsideAlerter>
    </>
  );
}
