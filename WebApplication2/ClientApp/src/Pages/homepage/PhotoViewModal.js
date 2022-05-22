import React from "react";
import { Modal } from "react-bootstrap";

export default function PhotoViewModal(props) {
  const [show, setShow] = React.useState(props.show);
  const handleClose = () => setShow(false);
  const photo = props.photo;

  return (
    <>
      <Modal show={show} onHide={handleClose} centered>
        <Modal.Header closeButton style={{ border: "none" }}></Modal.Header>
        <Modal.Body>
          <img
            className='card-img'
            src={
              photo.includes("64") || photo.includes("blob")
                ? photo
                : require(`${photo}`)
            }
            alt='img'
          />
        </Modal.Body>
      </Modal>
    </>
  );
}
