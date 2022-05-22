import React from "react";
import Contact from "./Contact";
import Utilsbuttons from "./Utilsbuttons";
import { useContext, useState } from "react";
import AppContext from "../../AppContext";
import { ListGroup } from "react-bootstrap";

// the props are setChanges - used to re-render homepage
// and setActive to set current active user i talk with
const Contactslist = (props) => {
  const sharedContext = useContext(AppContext);
  // we are fetching contacts list in Homepage
  // {id, name, server, last, lastdate}
  const convertTime = (toConvert) => {
    if (toConvert) {
      const formatted = new Date(toConvert)
      return (formatted.getHours() + ':' + ("0" + formatted.getMinutes()).slice(-2));
    }
  }

  return (
    <>
      <Utilsbuttons setter={props.setter} />
      <div className='contact-list'>
        <div className='list-group'>
          <ListGroup>
            {(Array.from(props.contactsList)).map(
              // each item is JSON object - {id, name, server, last, lastdate}
              item => (
                <ListGroup.Item
                  active
                  style={{ display: "contents" }}
                  onClick={() => {
                    // @ts-ignore/
                    props.setActive(item.id);
                    sharedContext.activeContact = item.id;
                    props.setter(true);
                  }}
                >
                  <Contact
                    contactName={item.name}
                    // TODO: change to default photo???
                    photo='default.jpg'
                    lastMessage={item.last}
                    lastMessageTime={convertTime(item.lastdate)}

                  // TODO: the API doesnt support who sent last message
                  // isSelf={
                  //   sharedContext.userData[currUser].contacts[item].length ==
                  //   0
                  //     ? false
                  //     : sharedContext.userData[currUser].contacts[item].at(-1)
                  //         .isSelf
                  // }
                  />
                </ListGroup.Item>
              )
            )}
          </ListGroup>
        </div>
      </div>
    </>
  );
};

export default Contactslist;
