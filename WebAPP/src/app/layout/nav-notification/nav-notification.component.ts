import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  OnInit,
} from "@angular/core";
import { finalize } from "rxjs/operators";
import { NotificationService } from "./notification.service";

@Component({
  selector: "app-nav-notification",
  templateUrl: "./nav-notification.component.html",
  styles: [
    `
      .noti-scroll {
        overflow-y: scroll;
      }
      .notify-item-nodata {
        min-height: 100px;
        display: flex;
        justify-content: center;
        opacity: 0.5;
      }
      .notification-list {
        margin-left: 0;
      }
      .notification-list .noti-title {
        padding: 15px 20px;
      }
      .notification-list .noti-icon-badge {
        display: inline-block;
        position: absolute;
        top: 16px;
        right: 10px;
      }
      .notification-list .notify-item {
        padding: 12px 20px;
        border: 1px solid #e8e8e8;
      }
      .notification-list .notify-item.active {
        background: #f3f7f9;
      }

      .notification-list .notify-item .notify-icon {
        float: left;
        height: 36px;
        width: 36px;
        font-size: 18px;
        line-height: 36px;
        text-align: center;
        margin-right: 10px;
        border-radius: 50%;
        color: #fff;
      }
      .notification-list .notify-item .notify-details {
        margin-bottom: 5px;
        overflow: hidden;
        text-overflow: ellipsis;
        white-space: nowrap;
        color: var(--ct-gray-800);
      }
      .notification-list .notify-item .notify-details b {
        font-weight: 500;
      }
      .notification-list .notify-item .notify-details small {
        display: block;
      }
      .notification-list .notify-item .notify-details span {
        display: block;
        overflow: hidden;
        text-overflow: ellipsis;
        white-space: nowrap;
        font-size: 13px;
      }
      .notification-list .notify-item .user-msg {
        white-space: normal;
        line-height: 16px;
      }
      .notification-list .profile-dropdown .notify-item {
        padding: 7px 20px;
      }
      .noti-list {
        position: absolute;
        top: 10px;
        right: 150px;
        .noti-content {
          cursor: pointer;
          border-radius: 8px;
          border: 1px solid #8bc7ff;
          width: 48px;
          height: 48px;
          display: flex;
          align-items: center;
          justify-content: center;
          i {
            font-size: 26px;
          }
        }
      }
    `,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NavNotificationComponent implements OnInit {
  totalCount = 0;
  listNotification: any[] = [];
  open = true;
  loading = false;
  totalCountUnRead = 0;

  constructor(
    private cdr: ChangeDetectorRef,
    private sp: NotificationService
  ) {}

  ngOnInit(): void {
    this.sp.getCount().subscribe((val) => {
      if (val.isSuccessful) {
        this.totalCountUnRead = val.result;
        this.cdr.detectChanges();
      }
    });
  }

  getNotification() {
    this.loading = true;
    this.sp
      .getList({
        maxResultCount: 5,
        skipCount: 0,
        filter: "",
      })
      .pipe(
        finalize(() => {
          this.loading = false;
          this.cdr.detectChanges();
        })
      )
      .subscribe((val) => {
        if (val.isSuccessful) {
          this.listNotification = val.result.items.map((item) => {
            var timeSince = this.timeSince(item.sendTime);
            return { ...item, timeSince: timeSince };
          });
          this.totalCount = val.result.totalCount;
          this.cdr.markForCheck();
        }
      });
  }

  clearAll() {
    this.sp.readAll().subscribe();
    this.listNotification = this.listNotification.map((e) => {
      return { ...e, isRead: true };
    });
    this.totalCountUnRead = 0;
  }

  seeMore() {
    this.loading = true;
    this.sp
      .getList({
        maxResultCount: 5,
        skipCount: this.listNotification.length,
        filter: "",
      })
      .pipe(
        finalize(() => {
          this.loading = false;
          this.cdr.detectChanges();
        })
      )
      .subscribe((val) => {
        if (val.isSuccessful) {
          this.listNotification = [
            ...this.listNotification,
            ...val.result.items.map((item) => {
              var timeSince = this.timeSince(item.sendTime);
              return { ...item, timeSince: timeSince };
            }),
          ];
          this.totalCount = val.result.totalCount;
          this.cdr.markForCheck();
        }
      });
  }

  timeSince(date) {
    const now = new Date();
    var seconds = Math.floor(
      (now.getTime() - new Date(date + "").getTime()) / 1000
    );

    var interval = seconds / 31536000;

    if (interval > 1) {
      return Math.floor(interval) + " years";
    }
    interval = seconds / 2592000;
    if (interval > 1) {
      return Math.floor(interval) + " months";
    }
    interval = seconds / 86400;
    if (interval > 1) {
      return Math.floor(interval) + " days";
    }
    interval = seconds / 3600;
    if (interval > 1) {
      return Math.floor(interval) + " hours";
    }
    interval = seconds / 60;
    if (interval > 1) {
      return Math.floor(interval) + " minutes";
    }
    return Math.floor(seconds) + " seconds";
  }
}
