import React from "react";
export default function FileUpload({ cb, errorCb }) {
  function UploadFile(e) {
    const file = e.target.files[0];
    const size = Math.floor(file.size / 1024 ** 2);
    if (validFileType(file) && size <= 5) {
      cb(file);
      errorCb("");
    } else {
      errorCb("Please Upload Log File No Bigger Than 5mb");
      e.target.value = null;
    }
  }
  function validFileType(file) {
    const fileTypes = ["log"];
    const type = file.name.substring(file.name.lastIndexOf(".") + 1);
    return fileTypes.includes(type);
  }
  return <input type="file" onInput={UploadFile} name="file" />;
}
